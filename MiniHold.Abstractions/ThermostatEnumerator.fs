namespace MiniHold.Abstractions

open FSharp.Control
open I8Beef.Ecobee
open I8Beef.Ecobee.Protocol.Objects
open I8Beef.Ecobee.Protocol.Thermostat

module ThermostatEnumerator =
    let AsyncFind(client: IClient) = asyncSeq {
        let! ct = Async.CancellationToken
        let mutable page = 1
        let mutable finished = false
        while not finished do
            let request = new ThermostatRequest()
            request.Page <- new Page(CurrentPage = page)
            request.Selection <- new Selection(SelectionType = "registered", SelectionMatch = "")
            request.Selection.IncludeLocation <- true
            request.Selection.IncludeVersion <- true
            let! response = client.GetAsync<ThermostatRequest, ThermostatResponse>(request, ct) |> Async.AwaitTask
            for t in response.ThermostatList do
                yield new ThermostatClient(client, t)
            if request.Page.TotalPages.HasValue && request.Page.TotalPages.Value <= page then
                page <- request.Page.TotalPages.Value
            else
                finished <- true
    }

    let FindAsync(client) = AsyncFind(client) |> AsyncSeq.toAsyncEnum
    let FindAllAsync(client) = AsyncFind(client) |> AsyncSeq.toListAsync |> Async.StartAsTask
