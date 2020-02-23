using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;
using WorkScheduler.Services;

namespace WorkScheduler.ViewModels
{
    public class ProjectManagementPageViewModel : BasePageViewModel
    {
        
        public bool CapacityClicked { get; set; }

        public JobSchedule SelectedSchedule { get; set; }

        public ProjectManagementPageViewModel(ICacheService cacheService) : base(cacheService)
        {
            CapacityClicked = false;
        }

        public override async Task GetJobSchedules()
        {
            await base.GetJobSchedules();
            CapacityClicked = true;
        }

        public async Task SetSelectedSchedule (string name)
        {
            await GetJobSchedules();
            SelectedSchedule = JobSchedules.FirstOrDefault(js => js.JobName == name);
        }

        public async Task SaveSelectedSchedule(string name = null)
        {
           // var theOne = JobSchedules.SingleOrDefault(js => js.JobName == name);
            if (SelectedSchedule != null)
               await _cacheService.SaveSelectedJobSchedule(SelectedSchedule);
        }
    }
}
