using System;
using System.Collections.Generic;

namespace WorkScheduler.Models
{
    public class Phase
    {
        public string PhaseName => $"Phase {Index}";

        public  Guid JobId { get; set; }
        public int Index { get; set; }

        public List<BuildingSystem> BuildingSystems { get; set; }

        public Phase()
        {
        }
    }
}
