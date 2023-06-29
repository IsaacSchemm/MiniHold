using I8Beef.Ecobee;
using MiniHold.Abstractions;

namespace MiniHold.App
{
    public class ThermostatObject
    {
        public ThermostatObject(ThermostatClient t)
        {
            this.t = t;
        }

        private readonly ThermostatClient t;
        private ThermostatInformation i;

        public ThermostatClient ThermostatClient => t;
        public ThermostatInformation Information => i;

        public DateTimeOffset LastUpdated { get; private set; }

        private async Task Act(Func<Task> func)
        {
            ClientStatic.Busy++;
            await func();
            i = await ThermostatClient.GetInformationAsync();
            LastUpdated = DateTimeOffset.Now;
            ClientStatic.Busy--;
        }

        public Task Refresh() =>
            Act(() => Task.CompletedTask);

        public Task Hold10Heat() =>
            Act(async () => await QuickActions.SetTemperatureOffsetAsync(t, i.Desired, 3, TimeSpan.FromMinutes(10)));

        public Task Hold10Cool() =>
            Act(async () => await QuickActions.SetTemperatureOffsetAsync(t, i.Desired, -3, TimeSpan.FromMinutes(10)));

        public Task Hold10Fan() =>
            Act(async () => await QuickActions.SetFanAsync(t, i.Desired, true, TimeSpan.FromMinutes(10)));

        public Task Hold10Away() =>
            Act(async () => await QuickActions.SetAwayAsync(t, TimeSpan.FromMinutes(10)));

        public Task Hold30Heat() =>
            Act(async () => await QuickActions.SetTemperatureOffsetAsync(t, i.Desired, 3, TimeSpan.FromMinutes(30)));

        public Task Hold30Cool() =>
            Act(async () => await QuickActions.SetTemperatureOffsetAsync(t, i.Desired, -3, TimeSpan.FromMinutes(30)));

        public Task Hold30Fan() =>
            Act(async () => await QuickActions.SetFanAsync(t, i.Desired, true, TimeSpan.FromMinutes(30)));

        public Task Hold30Away() =>
            Act(async () => await QuickActions.SetAwayAsync(t, TimeSpan.FromMinutes(30)));

        public Task Hold2HAway() =>
            Act(async () => await QuickActions.SetAwayAsync(t, TimeSpan.FromHours(2)));

        public Task Hold4HAway() =>
            Act(async () => await QuickActions.SetAwayAsync(t, TimeSpan.FromHours(4)));

        public Task Hold6HAway() =>
            Act(async () => await QuickActions.SetAwayAsync(t, TimeSpan.FromHours(6)));

        public Task Hold7AAway() =>
            Act(async () => await QuickActions.SetAwayUntilTimeAsync(t, DateTime.Parse("7:00 AM")));

        public Task Hold4PAway() =>
            Act(async () => await QuickActions.SetAwayUntilTimeAsync(t, DateTime.Parse("4:00 PM")));

        public Task Hold9PAway() =>
            Act(async () => await QuickActions.SetAwayUntilTimeAsync(t, DateTime.Parse("9:00 PM")));

        public Task ClearHold() =>
            Act(async () => await t.CancelHoldAsync());
    }
}
