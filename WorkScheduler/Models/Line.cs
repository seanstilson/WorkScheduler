using System;
namespace WorkScheduler.Models
{
    public class Line
    {
        public string LineName { get; set; }
        public int DefaultBoardFeet { get; set; }
        public int ActualBoardFeet { get; set; }

        public Line()
        {
        }
    }
}
