using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WorkScheduler.Pages
{
    public partial class NewJobPage : ContentPage
    {
        public NewJobPage()
        {
            InitializeComponent();
        }

        void CheckBox_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            (sender as CheckBox).IsChecked = true;
        }
    }
}
