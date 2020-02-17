using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkScheduler.Models;
using WorkScheduler.Services;

namespace WorkScheduler.ViewModels
{
    public class HomePageViewModel : BasePageViewModel
    {

        public HomePageViewModel(ICacheService cacheService) : base(cacheService)
        {
        }

        public async Task<ObservableCollection<WorkScheduleItem>> GetWorkItemsAsync()
        {
            return await _cacheService.GetAllWorkItems();
        }
    }
}
