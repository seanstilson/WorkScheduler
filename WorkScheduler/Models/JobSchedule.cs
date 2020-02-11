using System;
namespace WorkScheduler.Models
{
    public class JobSchedule
    {
        public Guid Id { get; set; }

        public WorkScheduleItem ProjectManagementItem { get; set; }
        public WorkScheduleItem DesignItem { get; set; }
        public WorkScheduleItem ProductionItem { get; set; }
        public WorkScheduleItem TransportationItem { get; set;}
        public WorkScheduleItem ReviewItem { get; set; }

        public JobSchedule()
        {
            Id = Guid.NewGuid();
        }
    }
}
