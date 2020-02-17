using System;
using WorkScheduler.Models;
using WorkScheduler.Services;

namespace WorkScheduler.ViewModels
{
    public class ScheduleSelectorViewModel : BasePageViewModel
    {
        public ScheduleSelectorViewModel(ICacheService cacheService) : base (cacheService)
        {
        }
    }
}
