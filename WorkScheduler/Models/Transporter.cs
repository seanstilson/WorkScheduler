using System;
namespace WorkScheduler.Models
{
    public class Transporter : Assignee
    {
        public Transporter(Assignee assignee)
        {
            this.AssigneeName = assignee.AssigneeName;

            Department = new Department(WorkScheduler.Constants.TransportationDepartmentID)
            {
                DepartmentName = Enums.Enumerations.Department.Transportation.ToString()
            };

            this.Capacity = assignee.Capacity;
            this.Id = assignee.Id;
        }

        public Transporter()
        {
            Department = new Department(WorkScheduler.Constants.TransportationDepartmentID)
            {
                DepartmentName = Enums.Enumerations.Department.Transportation.ToString()
            };
        }
    }
}
