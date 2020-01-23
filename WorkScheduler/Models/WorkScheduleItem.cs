using System;
using Xamarin.Forms;

namespace WorkScheduler.Models
{
    public class WorkScheduleItem
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public DateTime From { get; set; }
        public DateTime OriginalStartDate { get; set; }
        public DateTime To { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public Color Color { get; set; }
        public Department Department { get; set; }
        public Assignee Assignee { get; set; }
        public bool IsAllDay { get; set; }
        public DateTime OriginalEndDate { get; set; }

        public WorkScheduleItem()
        {
            IsAllDay = true;
        }
    }
}
