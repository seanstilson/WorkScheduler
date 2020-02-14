using System;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using WorkScheduler.Services;
using WorkScheduler.ViewModels;
using CommonServiceLocator;

namespace WorkScheduler
{
    public class Bootstrap
    {
        public Bootstrap()
        {
        }
        public static void Initialize()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<HomePageViewModel>().AsSelf();
            builder.RegisterType<JobInfoPageViewModel>().AsSelf();
            builder.RegisterType<MainPageViewModel>().AsSelf();
            builder.RegisterType<AssignmentPageViewModel>().AsSelf();
            builder.RegisterType<ProjectManagementPageViewModel>().AsSelf();
            builder.RegisterType<CacheService>().As<ICacheService>();
            builder.RegisterType<WorkScheduleService>().As<IWorkScheduleService>();
            builder.RegisterType<EmailService>().As<IEmailService>();
            builder.RegisterType<CapacityEngine>().As<ICapacityEngine>();
            builder.RegisterType<AssigneeService>().As<IAssigneeService>();

            IContainer container = builder.Build();
            AutofacServiceLocator asl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => asl);
        }
    }
}
