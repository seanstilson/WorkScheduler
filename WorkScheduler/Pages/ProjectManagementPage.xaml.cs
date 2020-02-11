using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WorkScheduler.Pages
{
    public partial class ProjectManagementPage : ContentPage
    {
        public ProjectManagementPage()
        {
            InitializeComponent();
        }

        void Capacity_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AssignmentPage("ProjectManagement"));
        }
    }
}
