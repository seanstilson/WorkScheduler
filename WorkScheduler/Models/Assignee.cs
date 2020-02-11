using System;
namespace WorkScheduler.Models
{
    public class Assignee
    {
        public string AssigneeName { get; set; }
        public Guid Id { get; set; }
        public int Capacity { get; set; }
        public Department Department { get; set; }


        public string CapacityString => $"{AssigneeName} - \t{Capacity}";

        public string ShortCapacity => $"  {Capacity}";

        public Assignee()
        {
        }
    }
}
