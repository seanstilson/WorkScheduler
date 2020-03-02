using System;
using System.Collections.Generic;
using WorkScheduler.ViewModels;
using Xamarin.Forms;

namespace WorkScheduler.Pages
{
    public partial class NewJobPage : ContentPage
    {
        public JobInfoPageViewModel ViewModel => this.BindingContext as JobInfoPageViewModel;

        public NewJobPage()
        {
            InitializeComponent();
        }

        void CheckBox_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            (sender as CheckBox).IsChecked = true;
        }

        async void New_Phase_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new PhaseCreationPage(ViewModel.JobInfoItem));
        }
    }
}
