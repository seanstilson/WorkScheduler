using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.Services
{
    public interface IDesignerService
    {
        ObservableCollection<Designer> GetDesigners();
    }
}
