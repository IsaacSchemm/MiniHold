using MiniHold.Abstractions;

namespace MiniHold.App
{
    public class ThermostatObject
    {
        public ThermostatObject(ThermostatClient t)
        {
            ThermostatClient = t;
        }

        public ThermostatClient ThermostatClient { get; }
        public ThermostatInformation Information { get; private set; }

        public DateTimeOffset LastUpdated { get; private set; }

        public DateTimeOffset LastUpdatedLocal => LastUpdated.ToLocalTime();

        public async Task Act(Func<Task> func)
        {
            await App.Act(async () =>
            {
                await func();
                Information = await ThermostatClient.GetInformationAsync();
                LastUpdated = DateTimeOffset.UtcNow;
            });
        }

        public Task Refresh() =>
            Act(() => Task.CompletedTask);

        public Task ClearHold() =>
            Act(async () => await ThermostatClient.CancelHoldAsync());

        public Task CreateVacation(string name, TempRange tempRange, DateTime startDate, DateTime endDate) =>
            Act(async () => await ThermostatClient.CreateVacationAsync(
                name,
                tempRange,
                ThermostatClient.ToThermostatTime(startDate),
                ThermostatClient.ToThermostatTime(endDate),
                0));

        public Task DeleteVacation(string name) =>
            Act(async () => await ThermostatClient.DeleteVacationAsync(name));
    }
}
