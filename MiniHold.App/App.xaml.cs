namespace MiniHold.App
{
    public partial class App : Application
    {
        public static MiniHoldPage CurrentPage { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnResume()
        {
            CurrentPage?.HandleResume();
            base.OnResume();
        }
    }
}