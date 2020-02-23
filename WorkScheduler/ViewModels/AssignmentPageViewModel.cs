using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkScheduler.Enums;
using WorkScheduler.Models;
using WorkScheduler.Services;
using Xamarin.Forms.Internals;

namespace WorkScheduler.ViewModels
{
    public class AssignmentPageViewModel : BasePageViewModel
    {
        IAssigneeService _assigneeService;
        public ObservableCollection<Designer> Designers { get; set; }
        public ObservableCollection<ProjectManager> ProjectManagers { get; set; }
        public ObservableCollection<Producer> Producers { get; set; }
        public ObservableCollection<Transporter> Transporters { get; set; }
        public WorkScheduleItem WorkScheduleItem { get; set; }
        public JobSchedule JobScheduleItem { get; set; }
        public string Title { get; set; }
        public string EstBoardFt => $"Est BDFT - {WorkScheduleItem?.BoardFeetString}";
        public string AssigneeString {
            get {
                switch (Department)
                {
                    case Enumerations.Department.Design:
                        return "Designer Assigned";
                    case Enumerations.Department.Production:
                        return "Production Manager Assigned";
                    case Enumerations.Department.ProjectManagement:
                        return "Project Manager Assigned";
                    case Enumerations.Department.Transportation:
                        return "Transportation manager assigned";
                    default: return "No assignmemnt";
                }
            }
        }
        private string _departmentName;
        public string DepartmentName
        {
            get { return _departmentName; }
            set
            {
                _departmentName = value;
                Department = (Enumerations.Department)Enum.Parse(typeof(Enumerations.Department), _departmentName);
            }
        }
        Enumerations.Department Department { get; set; }

        public void GetAssigneeList()
        {
            var assignees = _assigneeService.GetAssignees(DepartmentName);

            switch (Department)
            {
                case Enumerations.Department.Design:
                    Designers = new ObservableCollection<Designer>();
                    assignees.ForEach(a => Designers.Add(new Designer(a)));
                    break;
                case Enumerations.Department.ProjectManagement:
                    ProjectManagers = new ObservableCollection<ProjectManager>();
                    assignees.ForEach(a => ProjectManagers.Add(new ProjectManager(a)));
                    break;
                case Enumerations.Department.Production:
                    Producers = new ObservableCollection<Producer>();
                    assignees.ForEach(a => Producers.Add(new Producer(a)));
                    break;
                case Enumerations.Department.Transportation:
                    Transporters = new ObservableCollection<Transporter>();
                    assignees.ForEach(a => Transporters.Add(new Transporter(a)));
                    break;
                default: break;
            }
        }

        public AssignmentPageViewModel(IAssigneeService service, ICacheService cacheService) : base(cacheService)
        {
            _assigneeService = service;
        }

        public WorkScheduleItem GetSelectedWorkItem()
        {
          //  WorkScheduleItem = await _cacheService.GetWorkScheduleAsync();
            //Title = WorkScheduleItem.Description;

            switch (Department)
            {
                case Enumerations.Department.Design:
                    WorkScheduleItem = JobScheduleItem.DesignItem;
                    break;
                case Enumerations.Department.Production:
                    WorkScheduleItem = JobScheduleItem.ProductionItem;
                    break;
                case Enumerations.Department.Transportation:
                    WorkScheduleItem = JobScheduleItem.TransportationItem;
                    break;
                case Enumerations.Department.ProjectManagement:
                    WorkScheduleItem = JobScheduleItem.ProjectManagementItem;
                    break;
            }
            OnPropertyChanged("WorkScheduleItem");
            //OnPropertyChanged(Title);
            return WorkScheduleItem;
        }

        public async Task<JobSchedule> GetSelectedJobSchedule()
        {
            JobScheduleItem = await _cacheService.GetSelectedJobSchedule();
            Title = $"{DepartmentName} - {JobScheduleItem.JobName}";
            OnPropertyChanged("JobSCheduleItem");
            OnPropertyChanged(Title);
            return JobScheduleItem;
        }

        public async Task<bool> SaveWorkItemAsync()
        {
           return await  _cacheService.SaveWorkScheduleAsync(WorkScheduleItem);
        }

        public async Task SetAssignee(Assignee assignee)
        {
            WorkScheduleItem.Assignee = assignee;
           // var jobSchedule = await _cacheService.GetJobSchedule(WorkScheduleItem.JobScheduleId);
            switch (Department)
            {
                case Enumerations.Department.Design:
                    JobScheduleItem.DesignItem = WorkScheduleItem;
                    break;
                case Enumerations.Department.Production:
                    JobScheduleItem.ProductionItem = WorkScheduleItem;
                    break;
                case Enumerations.Department.ProjectManagement:
                    JobScheduleItem.ProjectManagementItem = WorkScheduleItem;
                    break;
                case Enumerations.Department.Transportation:
                    JobScheduleItem.TransportationItem = WorkScheduleItem;
                    break;

            }
            
            await _cacheService.SaveJobSchedule(JobScheduleItem);
        }
    }
}
