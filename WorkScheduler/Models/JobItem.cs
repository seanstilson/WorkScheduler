using System;
using System.Collections.ObjectModel;

namespace WorkScheduler.Models
{
    public class JobItem
    {
        public Guid JobID { get; set; }

        public string JobName { get; set; }

        public ObservableCollection<Phase> Phases { get; set; }

        public string SelectedJobType { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int RoundTripMiles { get; set; }

        public bool HasWindows { get; set; }
        public bool WindowsInstalled { get; set; }

        public DateTime WindowDeliveryDate { get; set; }

        public Double WallBoardFeet { get; set; }

        public Double FloorSquareFeet { get; set; }

        public JobItem()
        {
        }
    }
}
