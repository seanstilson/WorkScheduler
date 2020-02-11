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
        public WorkScheduleItem WorkScheduleItem { get; set; }
        public string Title { get; set; }
        public string EstBoardFt => $"Est BDFT - {WorkScheduleItem?.BoardFeetString}";
        private ICacheService _cacheService;
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
                default: break;
            }
        }

        public AssignmentPageViewModel(IAssigneeService service, ICacheService cacheService)
        {
            _assigneeService = service;
            _cacheService = cacheService;
              
        }

        public async Task<WorkScheduleItem> GetSelectedWorkItem()
        {
            WorkScheduleItem = await _cacheService.GetWorkScheduleAsync();
            Title = WorkScheduleItem.Description;
            OnPropertyChanged("WorkScheduleItem");
            OnPropertyChanged(Title);
            return WorkScheduleItem;
        }
        public async Task<bool> SaveWorkItemAsync()
        {
           return await  _cacheService.SaveWorkScheduleAsync(WorkScheduleItem);
        }

        public async Task SetDesigner(Designer designer)
        {
            WorkScheduleItem.Assignee = designer;
            var jobSchedule = await _cacheService.GetJobSchedule(WorkScheduleItem.JobScheduleId);
            jobSchedule.DesignItem = WorkScheduleItem;
            await _cacheService.SaveJobSchedule(jobSchedule);
        }
    }
}
