using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkScheduler.Models;
using WorkScheduler.Services;

namespace WorkScheduler.ViewModels
{
    public class HomePageViewModel : BasePageViewModel
    {
        IWorkScheduleService _workScheduleService;

        public HomePageViewModel(IWorkScheduleService workScheduleService)
        {
            _workScheduleService = workScheduleService;
        }

        public async Task<ObservableCollection<WorkScheduleItem>> GetWorkItemsAsync()
        {
            return await _workScheduleService.GetWorkScheduleItems();
        }
    }
}
