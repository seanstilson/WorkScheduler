using System;
using System.Threading.Tasks;
using WorkScheduler.Models;
using Akavache;
using System.Reactive.Linq;

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
    }
}
