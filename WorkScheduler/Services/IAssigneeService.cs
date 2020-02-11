using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.Services
{
    public interface IAssigneeService
    {
        IEnumerable<Assignee> GetAssignees(string department);
    }
}
