using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WorkScheduler.Models;
using WorkScheduler.Services;

namespace WorkScheduler.ViewModels
{
    public class BasePageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<JobSchedule> JobSchedules { get; set; }

        protected ICacheService _cacheService;

        public BasePageViewModel(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public virtual async Task GetJobSchedules()
        {
            JobSchedules = await _cacheService.GetJobSchedules();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
