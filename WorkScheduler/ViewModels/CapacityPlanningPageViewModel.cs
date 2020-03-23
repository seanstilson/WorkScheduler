using System;
using System.Collections.Generic;
using WorkScheduler.Models;
using WorkScheduler.Services;

namespace WorkScheduler.ViewModels
{
    public class CapacityPlanningPageViewModel : BasePageViewModel
    {
        public List<Line> WallLines { get; set; }
        public CapacityPlanningPageViewModel(ICacheService cacheService) : base(cacheService)
        {
            WallLines = new List<Line>
            {
                new Line
                {
                    LineName = "Line 1",
                    DefaultBoardFeet = 5000
                },
                new Line
                {
                    LineName = "Line 2",
                    DefaultBoardFeet = 5000
                },
                new Line
                {
                    LineName = "Line 3",
                    DefaultBoardFeet = 4500
                },
                new Line
                {
                    LineName = "Line 4",
                    DefaultBoardFeet = 0
                },
                new Line
                {
                    LineName = "Line 5",
                    DefaultBoardFeet = 0
                },
                new Line
                {
                    LineName = "Line 6",
                    DefaultBoardFeet = 0
                },
                new Line
                {
                    LineName = "Line 7",
                    DefaultBoardFeet = 0
                },
                new Line
                {
                    LineName = "Line 8",
                    DefaultBoardFeet = 0
                }
            };
        }
    }
}
