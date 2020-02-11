using System;
namespace WorkScheduler.Models
{
    public class ProjectManager : Assignee
    {
        public ProjectManager()
        {
            Department = new Department(WorkScheduler.Constants.ProjectManagementDepartmentID)
            {
                DepartmentName = Enums.Enumerations.Department.ProjectManagement.ToString()
            };
        }

        public ProjectManager(Assignee assignee)
        {
            this.AssigneeName = assignee.AssigneeName;

            Department = new Department(WorkScheduler.Constants.ProjectManagementDepartmentID)
            {
                DepartmentName = Enums.Enumerations.Department.ProjectManagement.ToString()
            };

            this.Capacity = assignee.Capacity;
            this.Id = assignee.Id;
        }
    }
}
