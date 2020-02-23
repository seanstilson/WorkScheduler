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
        Enums.Enumerations.Department Department { get; set; }
        Assignee SelectedAssignee { get; set; }

        public AssignmentPage(string department)
        {
            InitializeComponent();
            ViewModel.DepartmentName = department;
            Enums.Enumerations.Department dept;
            bool b = Enum.TryParse<Enums.Enumerations.Department>(department, out dept);
            Department = (b) ? dept : Enums.Enumerations.Department.Design;
        }

        protected async override void OnAppearing()
        {
            ViewModel.GetAssigneeList();
            if (ViewModel.DepartmentName == "ProjectManagement")
                designerList.ItemsSource = ViewModel.ProjectManagers;
            else if (ViewModel.DepartmentName == "Design")
                designerList.ItemsSource = ViewModel.Designers;
            else if (ViewModel.DepartmentName == "Production")
                designerList.ItemsSource = ViewModel.Producers;
            else if (ViewModel.DepartmentName == "Transportation")
                designerList.ItemsSource = ViewModel.Transporters;
            await ViewModel.GetSelectedJobSchedule();
            ViewModel.GetSelectedWorkItem();
            Title.SetBinding(Label.TextProperty, "Title");
            BoardFtLabel.SetBinding(Label.TextProperty, "EstBoardFt");
            base.OnAppearing();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

       void designerList_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            SelectedAssignee = e.SelectedItem as Assignee;
            // await ViewModel.SetDesigner(designer);
            
           // await DisplayAlert($"{sname} Assigned", $"{sname} successfully assigned", "Ok");
        }

        protected async void Ok_Clicked(object sender, EventArgs args)
        {
            if ( SelectedAssignee != null)
            {
                string sname = string.Empty;
                switch (Department)
                {
                    case Enums.Enumerations.Department.Design:
                        sname = "Designer";
                        break;
                    case Enums.Enumerations.Department.Production:
                        sname = "Production Manager";
                        break;
                    case Enums.Enumerations.Department.ProjectManagement:
                        sname = "Project Manager";
                        break;
                    case Enums.Enumerations.Department.Transportation:
                        sname = "Transportation Manager";
                        break;
                }
                await ViewModel.SetAssignee(SelectedAssignee);
                await DisplayAlert($"{sname} Assigned", $"{sname} successfully assigned", "Ok");
            }
                
            await Navigation.PopAsync();

        }
    }
}
