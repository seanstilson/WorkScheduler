﻿using System;
namespace WorkScheduler.Models
{
    public class Designer : Assignee
    {
        public int Capacity { get; set; }

        public string CapacityString => $"{Name} - \t{Capacity}";

        public string ShortCapacity => $"  {Capacity}";

        public Designer()
        {
            Department = new Department(WorkScheduler.Constants.DesignDepartmentID)
            {
                name = Enums.Enumerations.Department.Design.ToString()
            };
        }
    }
}
