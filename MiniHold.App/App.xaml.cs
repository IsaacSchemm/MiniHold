namespace MiniHold.App
{
    public partial class App : Application
    {
        public static MiniHoldPage CurrentPage { get; set; }

        private static int _jobs = 0;
        public static bool Busy => _jobs > 0;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static async Task Act(Func<Task> func)
        {
            _jobs++;
            CurrentPage?.SignalChange();

            try
            {
                await func();
            }
            finally
            {
                _jobs--;
                CurrentPage?.SignalChange();
            }
        }

        protected override async void OnResume()
        {
            await Act(async () =>
            {
                await ClientStatic.EstablishThermostatListAsync();
                await ClientStatic.EstablishThermostatInformationAsync();
            });
            base.OnResume();
        }
    }
}