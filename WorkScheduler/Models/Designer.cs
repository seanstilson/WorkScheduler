using System;
namespace WorkScheduler.Models
{
    public class Designer : Assignee
    {
        public int Capacity { get; set; }

        public string CapacityString => $"{Name} - {Capacity}";

        public Designer()
        {
            Department = new Department(WorkScheduler.Constants.DesignDepartmentID)
            {
                name = Enums.Enumerations.Department.Design.ToString()
            };
        }
    }
}
