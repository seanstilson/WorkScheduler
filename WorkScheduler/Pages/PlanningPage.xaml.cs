﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WorkScheduler.Controls;
using WorkScheduler.Models;
using WorkScheduler.ViewModels;
using Xamarin.Forms;

namespace WorkScheduler.Pages
{
    public partial class PlanningPage : ContentPage
    {
        public bool Received { get; set; }
        PlanningPageViewModel ViewModel => BindingContext as PlanningPageViewModel;
        public PlanningPage(ObservableCollection<JobSchedule> jobs)
        {
            InitializeComponent();
            scheduleSelector.JobSchedules = jobs;
            scheduleSelector.IsVisible = false;
            MessagingCenter.Subscribe<ScheduleSelector, Label>(this, "Tapped", (sender, arg) =>
            {
                if (!Received)
                    ScheduleSelector_TappedEvent(arg, new EventArgs());
            });
        }

        private async void ScheduleSelector_TappedEvent(object sender, EventArgs e)
        {
            goButton.IsVisible = true;
            Label label = sender as Label;
            await ViewModel.SaveSelectedSchedule(label.Text);
            Received = true;
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
