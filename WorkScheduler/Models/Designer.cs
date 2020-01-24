using System;
namespace WorkScheduler.Models
{
    public class Designer : Assignee
    {
        public int Capacity { get; set; }

        public Designer()
        {
            Department = new Department(WorkScheduler.Constants.DesignDepartmentID)
            {
                name = Enums.Enumerations.Department.Design.ToString()
            };
        }
    }
}
