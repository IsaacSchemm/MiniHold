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

        private async Task Act(Func<Task> func)
        {
            var p = App.CurrentPage;
            if (p != null)
                p.Busy++;
            await func();
            Information = await ThermostatClient.GetInformationAsync();
            LastUpdated = DateTimeOffset.UtcNow;
            if (p != null)
                p.Busy--;
        }

        public Task Refresh() =>
            Act(() => Task.CompletedTask);

        public Task Hold10Heat() =>
            Act(async () => await QuickActions.SetTemperatureOffsetAsync(ThermostatClient, Information.Desired, 2, TimeSpan.FromMinutes(10)));

        public Task Hold10Cool() =>
            Act(async () => await QuickActions.SetTemperatureOffsetAsync(ThermostatClient, Information.Desired, -2, TimeSpan.FromMinutes(10)));

        public Task Hold10Fan() =>
            Act(async () => await QuickActions.SetFanAsync(ThermostatClient, Information.Desired, true, TimeSpan.FromMinutes(10)));

        public Task Hold10Away() =>
            Act(async () => await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromMinutes(10)));

        public Task Hold30Heat() =>
            Act(async () => await QuickActions.SetTemperatureOffsetAsync(ThermostatClient, Information.Desired, 2, TimeSpan.FromMinutes(30)));

        public Task Hold30Cool() =>
            Act(async () => await QuickActions.SetTemperatureOffsetAsync(ThermostatClient, Information.Desired, -2, TimeSpan.FromMinutes(30)));

        public Task Hold30Fan() =>
            Act(async () => await QuickActions.SetFanAsync(ThermostatClient, Information.Desired, true, TimeSpan.FromMinutes(30)));

        public Task Hold30Away() =>
            Act(async () => await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromMinutes(30)));

        public Task Hold2HAway() =>
            Act(async () => await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromHours(2)));

        public Task Hold4HAway() =>
            Act(async () => await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromHours(4)));

        public Task Hold6HAway() =>
            Act(async () => await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromHours(6)));

        public Task Hold7AAway() =>
            Act(async () => await QuickActions.SetAwayUntilTimeAsync(ThermostatClient, DateTime.Parse("7:00 AM")));

        public Task Hold4PAway() =>
            Act(async () => await QuickActions.SetAwayUntilTimeAsync(ThermostatClient, DateTime.Parse("4:00 PM")));

        public Task Hold9PAway() =>
            Act(async () => await QuickActions.SetAwayUntilTimeAsync(ThermostatClient, DateTime.Parse("9:00 PM")));

        public Task ClearHold() =>
            Act(async () => await ThermostatClient.CancelHoldAsync());
    }
}
