using System;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.Services
{
    public interface ICapacityEngine
    {
        Task<JobSchedule> CalculateSchedules(JobItem JobInfoItem);
    }
}
