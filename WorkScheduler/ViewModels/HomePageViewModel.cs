using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkScheduler.Models;
using WorkScheduler.Services;

namespace WorkScheduler.ViewModels
{
    public class HomePageViewModel : BasePageViewModel
    {
        ICacheService _cacheService;

        public HomePageViewModel(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<ObservableCollection<WorkScheduleItem>> GetWorkItemsAsync()
        {
            return await _cacheService.GetAllWorkItems();
        }
    }
}
