using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WorkScheduler.Models;

namespace WorkScheduler.Services
{
    public class AssigneeService : IAssigneeService
    {
        public AssigneeService()
        {
        }

        public IEnumerable<Assignee> GetAssignees(string department)
        {
            var assembly = typeof(AssigneeService).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("WorkScheduler.Assignees.json");

            using (var reader = new System.IO.StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<List<Assignee>>(json);
                if (string.IsNullOrEmpty(department))
                    return data;

                var assignees = new ObservableCollection<Assignee>(data.Where(a => a.Department.DepartmentName == department));
                return assignees; 
            }
        }

    }
}
