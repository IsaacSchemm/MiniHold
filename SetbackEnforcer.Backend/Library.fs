namespace SetbackEnforcer.Backend

open System.Threading
open System.Threading.Tasks
open FSharp.Control
open Azure.Identity
open Azure.Security.KeyVault.Secrets
open Newtonsoft.Json
open I8Beef.Ecobee
open MiniHold.Abstractions

module Main =
    let internal azureCredential = new DefaultAzureCredential()
    let internal secretClient = new SecretClient(Keys.VaultUri, azureCredential)

    let internal getTokenAsync (cancellationToken: CancellationToken) = task {
        let! response = secretClient.GetSecretAsync("ecobeeJson", cancellationToken = cancellationToken)
        return JsonConvert.DeserializeObject<StoredAuthToken>(response.Value.Value)
    }

    let internal setTokenAsync (token: StoredAuthToken) (cancellationToken: CancellationToken) =
        task {
            let! response = secretClient.SetSecretAsync("ecobeeJson", JsonConvert.SerializeObject(token), cancellationToken)
            ignore response
        } :> Task

    let internal ecobeeClient = new Client(Keys.ApiKey, getTokenAsync, setTokenAsync)

    let RunOnceAsync () = task {
        let! list = ThermostatEnumerator.FindAllAsync ecobeeClient
        for thermostat in list do
            let! info = thermostat.GetInformationAsync()

            let activeComfortLevelRef =
                info.ComfortLevels
                |> Seq.where (fun x -> x.Active)
                |> Seq.map (fun x -> x.Name)
                |> Seq.tryHead
                |> Option.defaultValue null

            let activeHoldComfortLevelRef =
                info.Events
                |> Seq.where (fun e -> e.Running)
                |> Seq.map (fun e -> e.ComfortLevelRef)
                |> Seq.tryHead
                |> Option.defaultValue null

            if activeComfortLevelRef = "DynSetback" then
                let outdoorTemp = info.Weather.Temperature
                let (compressorMinOutdoorTemp, _) = info.AuxCrossover

                let desiredComfortLevelRef =
                    if outdoorTemp < compressorMinOutdoorTemp
                    then "sleep"
                    else "home"

                if activeHoldComfortLevelRef <> desiredComfortLevelRef then
                    do! thermostat.HoldAsync(HoldType.ComfortLevel desiredComfortLevelRef, HoldDuration.NextTransition)
    }