using System;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.Services
{
    public interface ICapacityEngine
    {
        Task CalculateSchedules(JobItem JobInfoItem);
    }
}
