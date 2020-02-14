using System;
using System.Drawing;
using System.Threading.Tasks;
using Akavache;
using Newtonsoft.Json;
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

        public Task<JobSchedule> CalculateSchedules(JobItem JobInfoItem)
        {
            CurrentJobItem = JobInfoItem;

            TimeSpan delta = JobInfoItem.DeliveryDate.Subtract(DateTime.Now);
            Double totalHours = delta.Days * 10 + delta.Hours;
            Double totalDesign = totalHours * 0.2;
            Double totalProduction = totalHours * 0.5;
            Double totalTransportation = totalHours * 0.2;
            Double totalReview = totalHours * 0.1;

            JobSchedule = new JobSchedule();
            JobSchedule.JobItemId = JobInfoItem.JobID;
            JobSchedule.JobName = JobInfoItem.JobName;

            JobSchedule.DesignItem = CreateScheduleItem(totalDesign, Enumerations.Department.Design);
            JobSchedule.ProductionItem = CreateScheduleItem(totalProduction, Enumerations.Department.Production);
            JobSchedule.TransportationItem = CreateScheduleItem(totalTransportation, Enumerations.Department.Transportation);
            JobSchedule.ReviewItem = CreateScheduleItem(totalReview, Enumerations.Department.FinalReview);
            JobSchedule.ProjectManagementItem = CreateScheduleItem(totalHours, Enumerations.Department.ProjectManagement);

            string crank = JsonConvert.SerializeObject(JobSchedule);
            
            return Task.FromResult(JobSchedule);
        }

        private WorkScheduleItem CreateScheduleItem(Double hours, Enumerations.Department department)
        {
            Double days = hours / 10;
            if (days < 1) days = 1;

            WorkScheduleItem item = new WorkScheduleItem() { Id = Guid.NewGuid(), JobScheduleId = JobSchedule.Id };
            item.Department = new Department(Guid.NewGuid()) { DepartmentName = department.ToString() };
            item.Description = $"{CurrentJobItem.JobName} - {item.Department.DepartmentName}";
            item.FromTime = TimeSpan.FromHours(7);
            item.ToTime = TimeSpan.FromHours(18);
            item.ItemName = CurrentJobItem.JobName;
            item.Assignee = null;
            item.EstimatedBoardFeet = (int)CurrentJobItem.FloorSquareFeet;

            switch (department)
            {
                case Enumerations.Department.Design:
                    { 
                        item.Color = Color.Green;
                        item.From = DateTime.Now;
                        item.To = item.From + TimeSpan.FromDays(days);
                        return item;
                    }
                case Enumerations.Department.Production:
                    {
                        item.Color = Color.Blue;
                        item.From = JobSchedule.DesignItem.To;
                        item.To = item.From + TimeSpan.FromDays(days);
                        return item;
                    }
                case Enumerations.Department.Transportation:
                    {
                        item.Color = Color.Orange;
                        item.From = JobSchedule.ProductionItem.To;
                        item.To = item.From + TimeSpan.FromDays(days);
                        return item;
                    }
                case Enumerations.Department.FinalReview:
                    {
                        item.Color = Color.Red;
                        item.From = JobSchedule.TransportationItem.To;
                        item.To = item.From + TimeSpan.FromDays(days);
                        return item;
                    }
                case Enumerations.Department.ProjectManagement:
                    {
                        item.Color = Color.Purple;
                        item.From = JobSchedule.DesignItem.From;
                        item.To = item.From + TimeSpan.FromDays(days);
                        return item;
                    }

                default: return null;
            }
        }
    }
}
