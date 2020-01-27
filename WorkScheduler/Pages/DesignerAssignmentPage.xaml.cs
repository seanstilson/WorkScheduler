using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using WorkScheduler.ViewModels;
using Xamarin.Forms;

namespace WorkScheduler.Pages
{
    public partial class DesignerAssignmentPage : ContentPage
    {
        public DesignerAssignmentViewModel ViewModel => BindingContext as DesignerAssignmentViewModel;
        public DesignerAssignmentPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await ViewModel.GetSelectedWorkItem();
            Title.SetBinding(Label.TextProperty, "Title");
            BoardFtLabel.SetBinding(Label.TextProperty, "EstBoardFt");
            base.OnAppearing();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }
    }
}
