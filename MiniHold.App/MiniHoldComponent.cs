using Microsoft.AspNetCore.Components;

namespace MiniHold.App
{
    public class MiniHoldPage : ComponentBase
    {
        public int Busy { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Busy++;
            await ClientStatic.EstablishThermostatListAsync();
            await ClientStatic.EstablishThermostatInformationAsync();
            Busy--;
            await base.OnInitializedAsync();
            App.CurrentPage = this;
        }

        public async void HandleResume()
        {
            Busy++;
            StateHasChanged();

            try
            {
                await ClientStatic.EstablishThermostatListAsync();
                await ClientStatic.EstablishThermostatInformationAsync();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Busy--;
                StateHasChanged();
            }
        }
    }
}
