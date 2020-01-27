using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkScheduler.Models;
using WorkScheduler.Services;

namespace WorkScheduler.ViewModels
{
    public class DesignerAssignmentViewModel : BasePageViewModel
    {
        IDesignerService _designerService;
        public ObservableCollection<Designer> Designers { get; set; }
        public WorkScheduleItem WorkScheduleItem { get; set; }
        public string Title { get; set; }
        public string EstBoardFt => $"Est BDFT - {WorkScheduleItem?.BoardFeetString}";
        private ICacheService _cacheService;

        public DesignerAssignmentViewModel(IDesignerService service, ICacheService cacheService)
        {
            _designerService = service;
            _cacheService = cacheService;
            Designers = _designerService.GetDesigners();
        }

        public async Task<WorkScheduleItem> GetSelectedWorkItem()
        {
            WorkScheduleItem = await _cacheService.GetWorkScheduleAsync();
            Title = WorkScheduleItem.Description;
            OnPropertyChanged("WorkScheduleItem");
            OnPropertyChanged(Title);
            return WorkScheduleItem;
        }
        public async Task<bool> SaveWorkItemAsync()
        {
           return await  _cacheService.SaveWorkScheduleAsync(WorkScheduleItem);
        }
    }
}
