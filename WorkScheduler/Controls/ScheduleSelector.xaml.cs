using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using WorkScheduler.Models;
using WorkScheduler.Services;
using Xamarin.Forms;

namespace WorkScheduler.Controls
{
    public partial class ScheduleSelector : ContentView
    {
        Label SelectedLabel { get; set; }
        private ObservableCollection<JobSchedule> _jobSchedules;
       
        public static readonly BindableProperty JobSchedulesProperty = BindableProperty.Create("JobSchedules", typeof(ObservableCollection<JobSchedule>), typeof(ScheduleSelector), null, BindingMode.TwoWay);

        // Properties
        public ObservableCollection<JobSchedule> JobSchedules { get { return (ObservableCollection<JobSchedule>)GetValue(JobSchedulesProperty); } set { SetValue(JobSchedulesProperty, value); } }


        // Declare the event.
        public void OnTapperTapped(object sender, TappedEventArgs e)
        {
            
        }

        public void SetVisible()
        {
            jobList.IsVisible = true;
        }

        public ScheduleSelector()
        {
            InitializeComponent();
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        { 
            base.LayoutChildren(x, y, width, height);
            var ix = jobList.ItemsSource; 
        }

        void JobNameTapper_Tapped(System.Object sender, System.EventArgs e)
        {
            Label l = sender as Label;
            ViewCell cell = l.Parent as ViewCell;
            l.BackgroundColor = Color.WhiteSmoke;
            if (SelectedLabel != null)
                SelectedLabel.BackgroundColor = Color.Transparent;
            SelectedLabel = l;

            MessagingCenter.Send<ScheduleSelector, Label>(this, "Tapped", l );
        }

    }
}
