﻿using System;
namespace WorkScheduler.Models
{
    public class JobItem
    {
        public string SelectedJobType { get; set; }
        public string SelectedPhase { get; set; }

        public string SelectedBuildingSystem { get; set; }

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