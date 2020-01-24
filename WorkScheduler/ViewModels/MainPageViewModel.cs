﻿using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;
using WorkScheduler.Services;
using Xamarin.Forms.Internals;

namespace WorkScheduler.ViewModels
{
    public class MainPageViewModel : BasePageViewModel
    {
        private ICacheService _cacheService;
        public ObservableCollection<WorkScheduleItem> WorkItems { get; set; }
        public ObservableCollection<WorkScheduleItem> TempDragItems { get; set; }
        public Department FilterDepartment { get; set; }
       

        public MainPageViewModel(ICacheService cacheService)
        {
            _cacheService = cacheService;
            TempDragItems = new ObservableCollection<WorkScheduleItem>();
        }

        public void SetEndTimesForWeekView()
        {
            TempDragItems.Clear();
            WorkItems.ForEach(wi =>
            {
                WorkScheduleItem temp = new WorkScheduleItem
                {
                    Id = wi.Id,
                    Color = wi.Color,
                    Department = wi.Department,
                    From = wi.From,
                    To = wi.From.AddHours(2),
                    ItemName = wi.ItemName,
                    FromTime = wi.FromTime,
                    ToTime = wi.ToTime,
                    Description = wi.Description,
                    IsAllDay = wi.IsAllDay,
                    OriginalStartDate = wi.From,
                    OriginalEndDate = wi.To
                };
                TempDragItems.Add(temp);
            });           
        }

        public void ResetWorkItemTimesForMonthView()
        {
            WorkItems.Clear();
            TempDragItems.ForEach(wi =>
            {
                WorkScheduleItem temp = new WorkScheduleItem
                {
                    Id = wi.Id,
                    Color = wi.Color,
                    Department = wi.Department,
                    From = wi.From,
                    To = wi.From + (wi.OriginalEndDate - wi.OriginalStartDate),
                    ItemName = wi.ItemName,
                    FromTime = wi.FromTime,
                    ToTime = wi.ToTime,
                    Description = wi.Description,
                    IsAllDay = wi.IsAllDay
                };
                WorkItems.Add(temp);
            });

        }

        public void OffsetAllWorkScheduleItems(WorkScheduleItem changedItem)
        {
            TimeSpan ts = changedItem.From - changedItem.OriginalStartDate;
            Enums.Enumerations.Department dept = (Enums.Enumerations.Department)Enum.Parse(typeof(Enums.Enumerations.Department), changedItem.Department.name);
            var siblings = TempDragItems.Where(
                        tdi => tdi.ParentId == changedItem.ParentId &&
                        ((Enums.Enumerations.Department)Enum.Parse(typeof(Enums.Enumerations.Department), tdi.Department.name)) > dept);

            siblings?.ForEach(wi =>
           {
               if ( wi != changedItem)
                    wi.From += ts;
           });
        }

        public async Task GetWorkItems()
        {
            WorkItems = await _cacheService.GetAllWorkItems();
            if (FilterDepartment != null)
            {
                var selectedItems = WorkItems.Where(wi => wi.Department.name == FilterDepartment.name).ToList();
                WorkItems.Clear();
                WorkItems = new ObservableCollection<WorkScheduleItem>(selectedItems);
            }
                
            WorkItems.ForEach(wi =>
            {
                switch (wi.Department.name)
                {
                    case "Design": wi.Color = Color.Green;
                        break;
                    case "Production": wi.Color = Color.Blue;
                        break;
                    case "Transportation": wi.Color = Color.Orange;
                        break;
                    case "Final Review": wi.Color = Color.Red;
                        break;
                }
                wi.OriginalStartDate = wi.From;
                wi.OriginalEndDate = wi.To;
            });
            OnPropertyChanged(nameof(WorkItems));
        }

    }
}