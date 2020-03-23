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
        public AssignmentPageViewModel AssigneeVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AssignmentPageViewModel>();
            }
        }
        public ProjectManagementPageViewModel ProjectPageVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectManagementPageViewModel>();
            }
        }

        public PlanningPageViewModel PlanningPageVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlanningPageViewModel>();
            }
        }
        public PhaseCreationPageViewModel PhaseCreateVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PhaseCreationPageViewModel>();
            }
        }
        public CapacityPlanningPageViewModel CapacityVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CapacityPlanningPageViewModel>();
            }
        }

    }
}
