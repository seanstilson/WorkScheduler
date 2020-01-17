using System;
using System.Drawing;
using System.Threading.Tasks;
using WorkScheduler.Enums;
using WorkScheduler.Models;

namespace WorkScheduler.Services
{
    public class CapacityEngine : ICapacityEngine
    {
        JobItem CurrentJobItem { get; set; }
        JobSchedule JobSchedule { get; set; }

        public CapacityEngine()
        {
        }

        public Task CalculateSchedules(JobItem JobInfoItem)
        {
            CurrentJobItem = JobInfoItem;

            TimeSpan delta = JobInfoItem.DeliveryDate.Subtract(DateTime.Now);
            Double totalHours = delta.Days * 10 + delta.Hours;
            Double totalDesign = totalHours * 0.2;
            Double totalProduction = totalHours * 0.5;
            Double totalTransportation = totalHours * 0.2;
            Double totalReview = totalHours * 0.1;

            JobSchedule = new JobSchedule();
            JobSchedule.DesignItem = CreateScheduleItem(totalDesign, Enumerations.Department.Design);

            return Task.CompletedTask;
        }

        private WorkScheduleItem CreateScheduleItem(Double hours, Enumerations.Department department)
        {
            Double days = hours / 10;
            if (days < 1) days = 1;

            WorkScheduleItem item = new WorkScheduleItem();
            item.Description = $"{CurrentJobItem.JobName} - {item.Department.name}";
            item.FromTime = TimeSpan.FromHours(7);
            item.ToTime = TimeSpan.FromHours(18);
            item.ItemName = CurrentJobItem.JobName;
            item.Assignee = null;

            switch (department)
            {
                case Enumerations.Department.Design:
                    {
                        item.Department = new Department(Guid.NewGuid()) { name = "Design" };  
                        item.Color = Color.Green;
                        item.From = DateTime.Now;
                        item.To = item.From + TimeSpan.FromDays(days);
                        return item;
                    }
                case Enumerations.Department.Production:
                    {
                        item.Department = new Department(Guid.NewGuid()) { name = "Production" };
                        item.Color = Color.Blue;
                        item.From = JobSchedule.DesignItem.To;
                        item.To = item.From + TimeSpan.FromDays(days);
                        return item;
                    }

                default: return null;
            }
        }
    }
}
