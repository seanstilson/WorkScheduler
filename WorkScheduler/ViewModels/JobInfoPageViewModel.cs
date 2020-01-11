using System;
using System.Collections.Generic;

namespace WorkScheduler.ViewModels
{
    public class JobInfoPageViewModel : BasePageViewModel
    {
        public JobInfoPageViewModel()
        {
            JobTypes = new List<string>()
            {
                "SF",
                "MF",
                "LA"
            };

            Phases = new List<string>()
            {
                "1",
                "2",
                "3",
                "4",
                "5"
            };

            BuildingSystems = new List<string>()
            {
                "F1", "F2","F3","F4","F5","W1","W2","W3","W4","W5","RT"
            };

            DeliveryDate = DateTime.Today;
            RoundTripMiles = 0;

        }

        public List<String> JobTypes { get; set; }
        public string SelectedJobType { get; set; }

        public List<string> Phases { get; set; }
        public string SelectedPhase { get; set; }

        public List<string> BuildingSystems { get; set; }
        public string SelectedBuildingSystem { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int RoundTripMiles { get; set; }
        public string RountTripMilesString => RoundTripMiles.ToString();
    }

}
