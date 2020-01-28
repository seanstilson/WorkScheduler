using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.Services
{
    public interface ICacheService
    {
        Task SaveJobItem(JobItem item);

        Task SaveJobSchedule(JobSchedule jobSchedule);

        Task<ObservableCollection<WorkScheduleItem>> GetAllWorkItems();

        Task<bool> SaveWorkScheduleAsync(WorkScheduleItem item);

        Task<WorkScheduleItem> GetWorkScheduleAsync();

        Task<JobSchedule> GetJobSchedule(Guid id);
    }
}
