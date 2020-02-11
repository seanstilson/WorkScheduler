using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using WorkScheduler.Models;
using WorkScheduler.ViewModels;
using Xamarin.Forms;

namespace WorkScheduler.Pages
{
    public partial class AssignmentPage : ContentPage
    {
        public AssignmentPageViewModel ViewModel => BindingContext as AssignmentPageViewModel;
        public AssignmentPage(string department)
        {
            InitializeComponent();
            ViewModel.DepartmentName = department;
        }

        protected async override void OnAppearing()
        {
            ViewModel.GetAssigneeList();
            designerList.ItemsSource = ViewModel.ProjectManagers;
            await ViewModel.GetSelectedWorkItem();
            Title.SetBinding(Label.TextProperty, "Title");
            BoardFtLabel.SetBinding(Label.TextProperty, "EstBoardFt");
            base.OnAppearing();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

       async void designerList_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var designer = e.SelectedItem as Assignee;
           // await ViewModel.SetDesigner(designer);
            await DisplayAlert("Designer assigned", "Designer successfully assigned", "Ok");
        }
    }
}
