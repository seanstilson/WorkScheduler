using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WorkScheduler.Controls;
using WorkScheduler.Models;
using WorkScheduler.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace WorkScheduler.Pages
{
    public partial class ProjectManagementPage : ContentPage
    {
        ProjectManagementPageViewModel ViewModel => BindingContext as ProjectManagementPageViewModel;
        
        ObservableCollection<JobSchedule> JobSchedules;
        bool Received = true;

        public ProjectManagementPage(ObservableCollection<JobSchedule> jobs)
        {
            InitializeComponent();
            scheduleSelector.JobSchedules = jobs;
            
            MessagingCenter.Subscribe<ScheduleSelector, Label>(this, "Tapped", (sender, arg) =>
            {
                if ( !Received )
                    ScheduleSelector_TappedEvent(arg, new EventArgs());
            });

            scheduleSelector.IsVisible = false;
        }

        private void T_Tapped(object sender, EventArgs e)
        {
            ScheduleSelector_TappedEvent(sender, e);
        }

        private async void ScheduleSelector_TappedEvent(object sender, EventArgs e)
        {
            goButton.IsVisible = true;
            Label label = sender as Label;
            await ViewModel.SaveSelectedSchedule(label.Text);
            Received = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private List<JobSchedule> MakeSchedulerJobSchedules()
        {
            var jobs = new List<JobSchedule>();
            ViewModel.JobSchedules.ForEach(js => jobs.Add(js));

            return jobs;
        }

        void Capacity_Clicked(System.Object sender, System.EventArgs e)
        {
            ViewModel.CapacityClicked = true;
            scheduleSelector.IsVisible = true;
            goButton.IsVisible = true;
            
        }

        void jobList_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            
        }

        void jobList_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
           
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
           
            Label label = sender as Label;
            ViewCell cell = label.Parent as ViewCell;
            label.BackgroundColor = Color.WhiteSmoke;
            

            await ViewModel.SaveSelectedSchedule(label.Text);

        }

        async void Go_Clicked(System.Object sender, System.EventArgs e)
        {
            /*jobList.IsVisible = false;
            listLabel.IsVisible = false;
            goButton.IsVisible = false; */
            scheduleSelector.IsVisible = false;
            goButton.IsVisible = false;

            await Navigation.PushAsync(new AssignmentPage("ProjectManagement"));
        }
    }
}
