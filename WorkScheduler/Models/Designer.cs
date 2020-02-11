using System;
namespace WorkScheduler.Models
{
    public class Designer : Assignee
    {
        
        public Designer(Assignee assignee)
        {
            this.AssigneeName = assignee.AssigneeName;

            Department = new Department(WorkScheduler.Constants.DesignDepartmentID)
            {
                DepartmentName = Enums.Enumerations.Department.Design.ToString()
            };

            this.Capacity = assignee.Capacity;
            this.Id = assignee.Id;
        }

        public Designer()
        {
            Department = new Department(WorkScheduler.Constants.DesignDepartmentID)
            {
                DepartmentName = Enums.Enumerations.Department.Design.ToString()
            };
        }
    }
}
