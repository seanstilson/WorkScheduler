using System;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.Services
{
    public interface ICacheService
    {
        Task SaveJobItem(JobItem item);
    }
}
