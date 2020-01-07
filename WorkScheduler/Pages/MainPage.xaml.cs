using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfSchedule.XForms;
using WorkScheduler.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace WorkScheduler
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        ScheduleAppointmentCollection AppointmentCollection;
        public ObservableCollection<WorkScheduleItem> WorkItems { get; set; }
        public Color SelectedColor { get; set; }
        public string SelectedText { get; set; }

        public MainPage()
        {
            InitializeComponent();

            WorkItems = new ObservableCollection<WorkScheduleItem>();

            AppointmentCollection = new ScheduleAppointmentCollection();

            WorkScheduleItem item = new WorkScheduleItem
            {
                Id = Guid.NewGuid(),
                ItemName = "Design Work Begins",
                Color = Color.Green,
                Description = "Design Work begins in earnest on the contracted job."
            };

            item.From = new DateTime(DateTime.Now.Year,

            DateTime.Now.Month, DateTime.Now.Day + 7, 10, 0, 0);

            item.To = item.From.AddHours(24);

            item.FromTime = TimeSpan.FromHours(8);

            item.ToTime = TimeSpan.FromHours(17);

            WorkItems.Add(item);

            /*ScheduleAppointment clientMeeting = new ScheduleAppointment();

            clientMeeting.StartTime = new DateTime(DateTime.Now.Year,

            DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);

            clientMeeting.EndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month,

            DateTime.Now.Day, 12, 0, 0);

            clientMeeting.Color = Color.Blue;

            clientMeeting.Subject = "ClientMeeting";

            AppointmentCollection.Add(clientMeeting);*/

            schedule.DataSource = WorkItems;

            schedule.AllowAppointmentDrag = true;

            schedule.CellTapped += Schedule_CellTapped;

            schedule.CellDoubleTapped += Schedule_CellDoubleTapped;

            schedule.AppointmentDragStarting += Schedule_AppointmentDragStarting;

            schedule.AppointmentDrop += Schedule_AppointmentDrop;

            NonAccessibleBlock block = new NonAccessibleBlock();
            block.StartTime = item.From.Hour;
            block.EndTime = item.To.Hour;
            SelectedColor = Color.Red;

            displayTypePicker.Items.Add("Month View");
            displayTypePicker.Items.Add("Week View");
            displayTypePicker.SelectedIndexChanged += DisplayTypePicker_SelectedIndexChanged;
            displayTypePicker.SelectedIndex = 0;

            MessagingCenter.Subscribe<NewAppointmentPage, WorkScheduleItem>(this, "Done", (sender, arg) =>
            {
                WorkScheduleItem workitem = arg as WorkScheduleItem;
                var wi = this.WorkItems.SingleOrDefault(w => w.Id == workitem.Id);
                if ( wi != null)
                    this.WorkItems.Remove(wi);
                this.WorkItems.Add(workitem);
                this.schedule.DataSource = WorkItems;
                this.InvalidateMeasure();
            });
            //schedule.WorkWeekViewSettings.NonAccessibleBlocks.Add(block);

        }

        private void DisplayTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (displayTypePicker.SelectedItem.ToString().StartsWith("Week", StringComparison.InvariantCultureIgnoreCase))
                schedule.ScheduleView = ScheduleView.WeekView;
            else
                schedule.ScheduleView = ScheduleView.MonthView;
        }

        private void Schedule_AppointmentDragStarting(object sender, AppointmentDragStartingEventArgs e)
        {
            int ix = 0;
            ix += 1;
        }

        private async void Schedule_CellDoubleTapped(object sender, CellTappedEventArgs e)
        {
            //throw new NotImplementedException();
            var workItem = await EditWorkItem(e);

            int ix = 0;
            ix++;
            
        }

        private async Task<WorkScheduleItem> EditWorkItem(CellTappedEventArgs e)
        {
            NewAppointmentPage page;
            if (e.Appointments != null && e.Appointments.Count > 0)
            {
                page = new NewAppointmentPage(e.Appointments.First() as WorkScheduleItem);
            }
            else
            {
                page = new NewAppointmentPage(e.Datetime, SelectedColor);
                page.SelectedText = SelectedText;
            }
            await Navigation.PushModalAsync(page);

            return page.WorkItem;
        }

        private void Schedule_AppointmentDrop(object sender, AppointmentDropEventArgs e)
        {
            var end = e.DropTime;
            var start = (e.Appointment as WorkScheduleItem).From;

            TimeSpan ts = end.Subtract(start);
 
            
            foreach(WorkScheduleItem wi in WorkItems)
            {
                wi.From = wi.From.Add(ts);
                wi.To = wi.To.Add(ts);
            }
                  
            WorkScheduleItem[] items = new WorkScheduleItem[WorkItems.Count];
            WorkItems.CopyTo(items, 0);
            WorkItems.Clear();
            
            foreach(WorkScheduleItem wi in items)
            {
                WorkItems.Add(wi);
            }
            this.schedule.DataSource = WorkItems;

           // if (schedule.WorkWeekViewSettings.NonAccessibleBlocks[0].StartTime == e.DropTime.Hour ||
            //    (schedule.WorkWeekViewSettings.NonAccessibleBlocks[0].StartTime - 1 == e.DropTime.Hour && e.DropTime.Minute > 0))
                

        }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            /*Debug.WriteLine("Cell was tapped!");
            
            WorkScheduleItem item = new WorkScheduleItem();
            item.Id = Guid.NewGuid();
            item.From = e.Datetime;
            item.To = e.Datetime.AddHours(2);
            item.Color = SelectedColor;
            item.ItemName = "Materials Selection";
            WorkItems.Add(item); */
        }

        protected void Button_Click(object sender, EventArgs args)
        {
            Button b = sender as Button;
            SelectedColor = b.BackgroundColor;
            SelectedText = b.Text;

        }
    }
}
