using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WorkScheduler.Models;
using Xamarin.Forms;

namespace WorkScheduler.Pages
{
    public partial class PlanningPage : ContentPage
    {
        public PlanningPage(ObservableCollection<JobSchedule> jobs)
        {
            InitializeComponent();
            scheduleSelector.JobSchedules = jobs;
            scheduleSelector.IsVisible = false;
        }

        void Calendar_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage(new Models.Department(Guid.NewGuid()) { DepartmentName = "Design" }));
        }

        void Designer_Clicked(System.Object sender, System.EventArgs e)
        {
            scheduleSelector.IsVisible = true;
            goButton.IsVisible = true;
           // Navigation.PushAsync(new AssignmentPage("Design"));
        }

        void Go_Clicked(System.Object sender, EventArgs args)
        {
            Navigation.PushAsync(new AssignmentPage("Design"));
        }

    }
}
