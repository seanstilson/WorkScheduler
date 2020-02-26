using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Navigation.PushAsync(new PhaseCreationPage());
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
    }
}
