using System;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Services;

namespace WorkScheduler.ViewModels
{
    public class PlanningPageViewModel : BasePageViewModel
    {
        public PlanningPageViewModel(ICacheService cacheService) : base (cacheService)
        {
        }

        public async Task SaveSelectedSchedule(string name)
        {
            await base.GetJobSchedules();
            var theOne = JobSchedules.SingleOrDefault(js => js.JobName == name);
            if (theOne != null)
                await _cacheService.SaveSelectedJobSchedule(theOne);
        }
    }
}
