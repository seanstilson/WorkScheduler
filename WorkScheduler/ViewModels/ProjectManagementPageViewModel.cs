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
        public ObservableCollection<JobSchedule> JobSchedules { get; set; }

        public bool CapacityClicked { get; set; }

        private ICacheService _cacheService;

        public ProjectManagementPageViewModel(ICacheService cacheService)
        {
            _cacheService = cacheService;
            CapacityClicked = false;
        }

        public async Task GetJobSchedules()
        {
            JobSchedules = await _cacheService.GetJobSchedules();
            CapacityClicked = true;
        }

        public async Task SaveSelectedSchedule(string name)
        {
            var theOne = JobSchedules.SingleOrDefault(js => js.JobName == name);
            if (theOne != null)
               await _cacheService.SaveSelectedJobSchedule(theOne);
        }
    }
}
