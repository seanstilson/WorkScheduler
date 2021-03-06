﻿using System;
namespace WorkScheduler.Models
{
    public class BuildingSystem
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public int EstimatedBoardFeet { get; set; }
        public int ActualBoardFeet { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string DeliveryDateString => DeliveryDate?.ToShortDateString();

        public BuildingSystem()
        {
        }
    }
}
