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
        }

        public void New_Job_Clicked(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new JobInfoPage());
        }

        void PMButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ProjectManagementPage());
        }

        void Schedule_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        void Design_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PlanningPage());
        }
    }
}
