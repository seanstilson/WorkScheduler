using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WorkScheduler.Models;
using WorkScheduler.ViewModels;
using Xamarin.Forms;

namespace WorkScheduler
{
    public partial class HomePage : ContentPage
    {
        private HomePageViewModel _viewModel => this.BindingContext as HomePageViewModel;

        public HomePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ObservableCollection<WorkScheduleItem> workItems = await _viewModel.GetWorkItemsAsync();
        }
    }
}
