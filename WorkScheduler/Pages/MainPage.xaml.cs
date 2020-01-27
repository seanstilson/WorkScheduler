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
using WorkScheduler.ViewModels;
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
        public ObservableCollection<WorkScheduleItem> WorkScheduleItems { get; set; }
        public Color SelectedColor { get; set; }
        public string SelectedText { get; set; }
        public MainPageViewModel ViewModel => BindingContext as MainPageViewModel;
        public Department FilterDepartment { get; set; }

        private bool PageLoaded { get; set; }

        public MainPage(Department filterDepartment = null)
        {
            PageLoaded = false;

            InitializeComponent();

            FilterDepartment = filterDepartment;

            // WorkScheduleItems = items;


            AppointmentCollection = new ScheduleAppointmentCollection();

            schedule.AllowAppointmentDrag = true;

            schedule.CellTapped += Schedule_CellTapped;

            schedule.CellDoubleTapped += Schedule_CellDoubleTapped;

            schedule.AppointmentDragStarting += Schedule_AppointmentDragStarting;

            schedule.AppointmentDrop += Schedule_AppointmentDrop;

            WeekViewSettings weekViewSeetings = new WeekViewSettings();
            weekViewSeetings.ShowAllDay = true;
            weekViewSeetings.AllDayAppointmentLayoutColor = Color.Beige;
            schedule.WeekViewSettings = weekViewSeetings;

            //NonAccessibleBlock block = new NonAccessibleBlock();
            //block.StartTime = item.From.Hour;
            //block.EndTime = item.To.Hour;
            //SelectedColor = Color.Red;

            displayTypePicker.Items.Add("Month View");
            displayTypePicker.Items.Add("Week View");
            displayTypePicker.SelectedIndexChanged += DisplayTypePicker_SelectedIndexChanged;
            displayTypePicker.SelectedIndex = 0;

            MessagingCenter.Subscribe<NewAppointmentPage, WorkScheduleItem>(this, "Done", (sender, arg) =>
            {
                WorkScheduleItem workitem = arg as WorkScheduleItem;
                /*var wi = this.WorkItems.SingleOrDefault(w => w.Id == workitem.Id);
                if ( wi != null)
                    this.WorkItems.Remove(wi);
                this.WorkItems.Add(workitem);
                this.schedule.DataSource = WorkItems;*/
                // this.InvalidateMeasure();
            });
            //schedule.WorkWeekViewSettings.NonAccessibleBlocks.Add(block);

        }

        protected override async void OnAppearing()
        {
            if (FilterDepartment != null)
                ViewModel.FilterDepartment = FilterDepartment;

            await ViewModel.GetWorkItems();
            //WorkItems = ViewModel.WorkItems;

            base.OnAppearing();

            PageLoaded = true;
            //this.InvalidateMeasure();
        }

        private void DisplayTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PageLoaded)
            {
                if (displayTypePicker.SelectedItem.ToString().StartsWith("Week", StringComparison.InvariantCultureIgnoreCase))
                {
                    ViewModel.SetEndTimesForWeekView();
                    schedule.DataSource = ViewModel.TempDragItems;
                    schedule.ScheduleView = ScheduleView.WeekView;
                }
                else
                {
                    ViewModel.ResetWorkItemTimesForMonthView();
                    schedule.DataSource = ViewModel.WorkItems;
                    schedule.ScheduleView = ScheduleView.MonthView;
                }
            }

        }

        private void Schedule_AppointmentDragStarting(object sender, AppointmentDragStartingEventArgs e)
        {
            var work = e.Appointment as WorkScheduleItem;

            //cranker.
            schedule.ResumeAppointmentUpdate();
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
            WorkScheduleItem item = e.Appointment as WorkScheduleItem;
            item.From = e.DropTime;
            item.To = item.From.AddHours(2);
            ViewModel.OffsetAllWorkScheduleItems(item);

            TimeSpan ts = end.Subtract(start);


            /* foreach(WorkScheduleItem wi in WorkItems)
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
             this.schedule.DataSource = WorkItems; */

            // if (schedule.WorkWeekViewSettings.NonAccessibleBlocks[0].StartTime == e.DropTime.Hour ||
            //    (schedule.WorkWeekViewSettings.NonAccessibleBlocks[0].StartTime - 1 == e.DropTime.Hour && e.DropTime.Minute > 0))


        }

        private async void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            if (e.Appointments == null || e.Appointments.Count < 1)
                return;
            await ViewModel.SaveSelectedWorkItemAsync(e.Appointments.First() as WorkScheduleItem);
        }

        protected void Button_Click(object sender, EventArgs args)
        {
            Button b = sender as Button;
            SelectedColor = b.BackgroundColor;
            SelectedText = b.Text;

        }

        async void Confirm_Clicked(System.Object sender, System.EventArgs e)
        {
            
        }
    }
}
