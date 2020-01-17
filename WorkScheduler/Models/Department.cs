using System;
using System.Collections.Generic;

namespace WorkScheduler.Models
{
    public class Department
    {
        public static List<string> Departments = new List<string> { "Project Management", "Design", "Production", "Transportation", "Final Review" };

        public Guid Id { get; set; }
        public string name { get; set; }

        public Department(Guid id)
        {
            Id = id;
        }
    }
}
