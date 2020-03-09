using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WorkScheduler.Models;
using WorkScheduler.Pages;
using WorkScheduler.ViewModels;
using Xamarin.Forms;

namespace WorkScheduler
{
    public partial class HomePage : ContentPage
    {
        private HomePageViewModel _viewModel => this.BindingContext as HomePageViewModel;

        ObservableCollection<WorkScheduleItem> WorkItems;

        public HomePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var forecasts = await GetWeatherForecast();

            WorkItems = await _viewModel.GetWorkItemsAsync();
            await _viewModel.GetJobSchedules();
        }

        public void New_Job_Clicked(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new NewJobPage());
        }

        void PMButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var jobs = _viewModel.JobSchedules;
            Navigation.PushAsync(new PhaseCreationPage(new JobItem()));
            //Navigation.PushAsync(new ProjectManagementPage(jobs));
        }

        void Schedule_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        void Design_Clicked(System.Object sender, System.EventArgs e)
        {
            var jobs = _viewModel.JobSchedules;
            Navigation.PushAsync(new PlanningPage(jobs, "Design"));
        }

        void Production_Clicked(System.Object sender, System.EventArgs e)
        {
            var jobs = _viewModel.JobSchedules;
            Navigation.PushAsync(new PlanningPage(jobs, "Production"));
        }

        void Transportation_Clicked(System.Object sender, System.EventArgs e)
        {
            var jobs = _viewModel.JobSchedules;
            Navigation.PushAsync(new PlanningPage(jobs, "Transportation"));
        }

        async Task<List<WeatherForecast>> GetWeatherForecast()
        {
            string url = "http://63bc8d5a.ngrok.io/weatherforecast";
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var forecasts = await response.Content.ReadAsStringAsync();
                List<WeatherForecast> weatherForecasts = JsonConvert.DeserializeObject<List<WeatherForecast>>(forecasts);
                return weatherForecasts;
            }
            return null;

        }
    }
}
