using System;
using System.Collections.Generic;
using System.Windows.Input;
using WorkScheduler.Models;
using Xamarin.Forms;
using WorkScheduler.Services;

namespace WorkScheduler.ViewModels
{
    public class JobInfoPageViewModel : BasePageViewModel
    {
        public ICommand OkClickedCommand { get; set; }
        public ICommand CancelClickedCommand { get; set; }

        public JobItem JobInfoItem { get; set; }
        private ICacheService _cacheService;

        public JobInfoPageViewModel(ICacheService cacheService)
        {
            _cacheService = cacheService;

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
            HasWindows = true;
            WindowsInstalled = false;
            WindowDeliveryDate = DateTime.Today;

            OkClickedCommand = new Command(
                execute: async () =>
                 {
                     JobInfoItem = new JobItem
                     {
                         DeliveryDate = DeliveryDate,
                         FloorSquareFeet = FloorSquareFeet,
                         HasWindows = HasWindows,
                         RoundTripMiles = RoundTripMiles,
                         SelectedBuildingSystem = SelectedBuildingSystem,
                         SelectedJobType = SelectedJobType,
                         SelectedPhase = SelectedPhase,
                         WallBoardFeet = WallBoardFeet,
                         WindowDeliveryDate = WindowDeliveryDate,
                         WindowsInstalled = WindowsInstalled
                     };

                    await _cacheService.SaveJobItem(JobInfoItem);
                    await App.Current.MainPage.Navigation.PopModalAsync();
                 });

            CancelClickedCommand = new Command(
                execute: () =>
                    {
                        JobInfoItem = null;
                        App.Current.MainPage.Navigation.PopModalAsync();
                    }
                );

        }

        public void TransferDataAndLeave()
        {
            JobInfoItem = new JobItem
            {
                DeliveryDate = DeliveryDate,
                FloorSquareFeet = FloorSquareFeet,
                HasWindows = HasWindows,
                RoundTripMiles = RoundTripMiles,
                SelectedBuildingSystem = SelectedBuildingSystem,
                SelectedJobType = SelectedJobType,
                SelectedPhase = SelectedPhase,
                WallBoardFeet = WallBoardFeet,
                WindowDeliveryDate = WindowDeliveryDate,
                WindowsInstalled = WindowsInstalled
            };

            App.Current.MainPage.Navigation.PopModalAsync();
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

        public bool HasWindows { get; set; }
        public bool WindowsInstalled { get; set; }

        public DateTime WindowDeliveryDate { get; set; }

        public Double WallBoardFeet { get; set; }
        public string WallFeetString => WallBoardFeet.ToString("N2");

        public Double FloorSquareFeet { get; set; }
        public string FloorFeetString => FloorSquareFeet.ToString("N2");

    }

}
