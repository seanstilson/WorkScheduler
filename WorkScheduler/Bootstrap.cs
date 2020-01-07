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
            builder.RegisterType<WorkScheduleService>().As<IWorkScheduleService>();

            IContainer container = builder.Build();
            AutofacServiceLocator asl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => asl);
        }
    }
}
