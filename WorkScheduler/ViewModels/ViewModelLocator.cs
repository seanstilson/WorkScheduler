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

        public JobInfoPageViewModel JobInfoPageVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<JobInfoPageViewModel>();
            }

        }

        public MainPageViewModel MainPageVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }
        public DesignerAssignmentViewModel DesignerVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DesignerAssignmentViewModel>();
            }
        }
    }
}
