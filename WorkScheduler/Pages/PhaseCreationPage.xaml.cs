using System;
using System.Collections.Generic;
using WorkScheduler.Models;
using WorkScheduler.ViewModels;
using Xamarin.Forms;

namespace WorkScheduler.Pages
{
    public partial class PhaseCreationPage : ContentPage
    {
        public PhaseCreationPageViewModel ViewModel => this.BindingContext as PhaseCreationPageViewModel;
        public JobItem JobItem { get; set; }

        public PhaseCreationPage(JobItem jobItem)
        {
            JobItem = jobItem;
            InitializeComponent();
            ViewModel.Phases = jobItem?.Phases;
        }

        void System_Add_Clicked(System.Object sender, System.EventArgs e)
        {
            Button b = sender as Button;
            ViewModel.AddNewBuildingSystem(b.AutomationId);
        }

        void New_Phase_Clicked(System.Object sender, System.EventArgs e)
        {
            ViewModel.AddNewPhase();
        }

        async void Button_Cancel_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Button_OK_Clicked(System.Object sender, System.EventArgs e)
        {
            if (JobItem == null)
                JobItem = new JobItem();
            JobItem.Phases = ViewModel.Phases;
            await ViewModel.SaveJobItem(JobItem);
            await Navigation.PopModalAsync();
        }
    }
}
