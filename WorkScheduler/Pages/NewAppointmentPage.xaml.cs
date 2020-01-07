using System;
using System.Collections.Generic;
using WorkScheduler.Models;
using Xamarin.Forms;

namespace WorkScheduler
{
    public partial class NewAppointmentPage : ContentPage
    {
        private WorkScheduleItem workScheduleItem;

        public string SelectedText { get { return WorkItem.ItemName; } set { WorkItem.ItemName = value; NameEntry.Text = value; } }

        public WorkScheduleItem WorkItem { get; set; }

        public NewAppointmentPage(DateTime dateTime, Color color)
        {
            WorkItem = new WorkScheduleItem
            {
                Id = Guid.NewGuid(),
                From = dateTime,
                To = dateTime,
                FromTime = new TimeSpan(8, 0, 0),
                ToTime = new TimeSpan(17, 0, 0),
                Color = color
            };
            this.BindingContext = WorkItem;
            InitializeComponent();
        }

        public NewAppointmentPage(WorkScheduleItem workScheduleItem)
        {
            this.WorkItem = workScheduleItem;

            this.BindingContext = WorkItem;
            InitializeComponent();
        }

        protected void Done_Clicked(object sender, EventArgs args)
        {
            MessagingCenter.Send<NewAppointmentPage, WorkScheduleItem>(this, "Done", this.WorkItem);
            
            Navigation.PopModalAsync();
        }

        protected void Cancel_Clicked(object sender, EventArgs args)
        {
            Navigation.PopModalAsync();
        }
    }
}
