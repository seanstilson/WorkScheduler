using System;
using System.Collections.Generic;

namespace WorkScheduler.Models
{
    public class ProductionSystem
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public List<Line> Lines { get; set; }
        public int ScheduledWorkDays { get; set; }
        public int TotalAssignedBoardFeet { get; set; }

        public ProductionSystem()
        {
        }
    }
}
