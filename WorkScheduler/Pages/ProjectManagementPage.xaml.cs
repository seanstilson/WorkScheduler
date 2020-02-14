using System;
using System.Collections.Generic;
using System.Linq;
using WorkScheduler.ViewModels;
using Xamarin.Forms;

namespace WorkScheduler.Pages
{
    public partial class ProjectManagementPage : ContentPage
    {
        ProjectManagementPageViewModel ViewModel => BindingContext as ProjectManagementPageViewModel;
        Label SelectedLabel { get; set; }

        public ProjectManagementPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void Capacity_Clicked(System.Object sender, System.EventArgs e)
        {
            await ViewModel.GetJobSchedules();
            jobList.ItemsSource = ViewModel.JobSchedules;
            jobList.IsVisible = true;
            listLabel.IsVisible = true;
            goButton.IsVisible = true;
            //await Navigation.PushAsync(new AssignmentPage("ProjectManagement"));
        }

        void jobList_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            
        }

        void jobList_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
           
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (SelectedLabel != null)
                SelectedLabel.BackgroundColor = Color.Transparent;

            Label label = sender as Label;
            ViewCell cell = label.Parent as ViewCell;
            label.BackgroundColor = Color.WhiteSmoke;
            SelectedLabel = label;

            await ViewModel.SaveSelectedSchedule(label.Text);

        }

        async void Go_Clicked(System.Object sender, System.EventArgs e)
        {
            jobList.IsVisible = false;
            listLabel.IsVisible = false;
            goButton.IsVisible = false;

            await Navigation.PushAsync(new AssignmentPage("ProjectManagement"));
        }
    }
}
