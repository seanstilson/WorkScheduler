using System;
namespace WorkScheduler.Models
{
    public class Assignee
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Department Department { get; set; }

        public Assignee()
        {
        }
    }
}
