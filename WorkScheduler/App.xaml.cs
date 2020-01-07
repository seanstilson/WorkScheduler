using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Akavache;

namespace WorkScheduler
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTg2NzQyQDMxMzcyZTM0MmUzMFpJMVNYU0J5Yi85eU1pNnFWK0RYbFhKUEtmVm5DVW0vTUdydUF5YzI5NEU9");
            Akavache.Registrations.Start("WorkScheduler");
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
