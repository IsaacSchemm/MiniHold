using Microsoft.AspNetCore.Components;

namespace MiniHold.App
{
    public class MiniHoldPage : ComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            await App.Act(async () =>
            {
                await ClientStatic.EstablishThermostatListAsync();
                await ClientStatic.EstablishThermostatInformationAsync();
            });
            await base.OnInitializedAsync();
            App.CurrentPage = this;
        }

        public void SignalChange()
        {
            StateHasChanged();
        }
    }
}
