using System;
using WorkScheduler.ViewModels;
using CommonServiceLocator;

namespace WorkScheduler.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            Bootstrap.Initialize();
        }

        public HomePageViewModel HomePageVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomePageViewModel>();
            }
           
        }
    }
}
