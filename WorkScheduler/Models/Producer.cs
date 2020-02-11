using System;
namespace WorkScheduler.Models
{
    public class Producer : Assignee
    {
        public Producer()
        {
            Department = new Department(WorkScheduler.Constants.ProductionDepartmentID)
            {
                DepartmentName = Enums.Enumerations.Department.Production.ToString()
            };
        }

        public Producer(Assignee assignee)
        {
            this.AssigneeName = assignee.AssigneeName;

            Department = new Department(WorkScheduler.Constants.ProductionDepartmentID)
            {
                DepartmentName = Enums.Enumerations.Department.Production.ToString()
            };

            this.Capacity = assignee.Capacity;
            this.Id = assignee.Id;
        }
    }
}
