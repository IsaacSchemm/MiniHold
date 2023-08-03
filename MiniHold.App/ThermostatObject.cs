using MiniHold.Abstractions;
using System.Globalization;

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

        private async Task Act(Func<Task> func)
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

        public Task Hold15Heat() =>
            Act(async () => await QuickActions.SetHeatAsync(ThermostatClient, TimeSpan.FromMinutes(15)));

        public Task Hold15Cool() =>
            Act(async () => await QuickActions.SetCoolAsync(ThermostatClient, TimeSpan.FromMinutes(15)));

        public Task Hold15Fan() =>
            Act(async () => await QuickActions.SetFanAsync(ThermostatClient, true, TimeSpan.FromMinutes(15)));

        public Task Hold30Heat() =>
            Act(async () => await QuickActions.SetHeatAsync(ThermostatClient, TimeSpan.FromMinutes(30)));

        public Task Hold30Cool() =>
            Act(async () => await QuickActions.SetCoolAsync(ThermostatClient, TimeSpan.FromMinutes(30)));

        public Task Hold30Fan() =>
            Act(async () => await QuickActions.SetFanAsync(ThermostatClient, true, TimeSpan.FromMinutes(30)));

        public Task Hold1HAway() =>
            Act(async () => await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromHours(1)));

        public Task Hold1DAway() =>
            Act(async () => await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromDays(1)));

        public Task Hold7DAway() =>
            Act(async () => await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromDays(7)));

        public Task Hold7AAway() =>
            Act(async () => await QuickActions.SetAwayUntilClockAsync(ThermostatClient, TimeSpan.FromHours(7)));

        public Task Hold4PAway() =>
            Act(async () => await QuickActions.SetAwayUntilClockAsync(ThermostatClient, TimeSpan.FromHours(12 + 4)));

        public Task Hold9PAway() =>
            Act(async () => await QuickActions.SetAwayUntilClockAsync(ThermostatClient, TimeSpan.FromHours(12 + 9)));

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
