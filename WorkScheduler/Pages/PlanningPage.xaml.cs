using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WorkScheduler.Pages
{
    public partial class PlanningPage : ContentPage
    {
        public PlanningPage()
        {
            InitializeComponent();
        }

        void Calendar_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage(new Models.Department(Guid.NewGuid()) { name = "Design" }));
        }

        void Designer_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new DesignerAssignmentPage());
        }
    }
}
