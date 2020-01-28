using System;
using System.Threading.Tasks;
using WorkScheduler.Models;
using Akavache;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;
using System.Diagnostics;
using System.Linq;

namespace WorkScheduler.Services
{
    public class CacheService : ICacheService
    {
        public CacheService()
        {
        }

        public async Task SaveJobItem(JobItem item)
        {
            await BlobCache.UserAccount.InsertObject("NewJobItem", item);
        }

        public async Task SaveJobSchedule(JobSchedule jobSchedule)
        {
            ObservableCollection<JobSchedule> jobList;
            try
            {
                jobList = await BlobCache.UserAccount.GetObject<ObservableCollection<JobSchedule>>("JobSchedules");
                
            }
            catch (KeyNotFoundException ex)
            {
                jobList = new ObservableCollection<JobSchedule>();
            }
            var ix = jobList.IndexOf(jobSchedule);
            if (ix >= 0)
                jobList[ix] = jobSchedule;
            else
                jobList.Add(jobSchedule);

            await BlobCache.UserAccount.InsertObject("JobSchedules",jobList);
        }

        public async Task<JobSchedule> GetJobSchedule(Guid id)
        {
            ObservableCollection<JobSchedule> jobList;
            try
            {
                jobList = await BlobCache.UserAccount.GetObject<ObservableCollection<JobSchedule>>("JobSchedules");
                var theOne = jobList.SingleOrDefault(j => j.Id == id);
                return theOne;
            }
            catch (KeyNotFoundException ex)
            {
                return null;
            }
        }

        public async Task<ObservableCollection<WorkScheduleItem>> GetAllWorkItems()
        {
            ObservableCollection<JobSchedule> jobList;
            ObservableCollection<WorkScheduleItem> workList = new ObservableCollection<WorkScheduleItem>();
            try
            {
                jobList = await BlobCache.UserAccount.GetObject<ObservableCollection<JobSchedule>>("JobSchedules");
                jobList.ForEach(ji =>
                {
                    workList.Add(ji.DesignItem);
                    workList.Add(ji.ProductionItem);
                    workList.Add(ji.TransportationItem);
                    workList.Add(ji.ReviewItem);
                });
                return workList;
            }
            catch (KeyNotFoundException ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return workList;
            }
        }

        public async Task<bool> SaveWorkScheduleAsync(WorkScheduleItem item)
        {
            try
            {
                await BlobCache.UserAccount.InsertObject("SelectedWorkSchedule", item);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public async Task<WorkScheduleItem> GetWorkScheduleAsync()
        {
            try
            {
                var work = await BlobCache.UserAccount.GetObject<WorkScheduleItem>("SelectedWorkSchedule");
                return work;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
