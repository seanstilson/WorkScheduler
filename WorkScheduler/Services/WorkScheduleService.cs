using System;
using System.Collections.ObjectModel;
using WorkScheduler.Models;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;

namespace WorkScheduler.Services
{
    public class WorkScheduleService : IWorkScheduleService
    {
        public WorkScheduleService()
        {
        }

        public async Task<ObservableCollection<WorkScheduleItem>> GetWorkScheduleItems()
        {
            try
            {
                var items = await BlobCache.UserAccount.GetObject<ObservableCollection<WorkScheduleItem>>("WorkItems");
                return items;
            }
            catch (System.Exception ex)
            {
                return new ObservableCollection<WorkScheduleItem>();
            }
            
        }
    }
}
