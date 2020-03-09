using System;
using System.Collections.Generic;
using System.Windows.Input;
using WorkScheduler.Models;
using Xamarin.Forms;
using WorkScheduler.Services;
using System.Threading.Tasks;
using WorkScheduler.Pages;
using System.Collections.ObjectModel;
using System.Linq;

namespace WorkScheduler.ViewModels
{
    public class JobInfoPageViewModel : BasePageViewModel
    {
        public ICommand OkClickedCommand { get; set; }
        public ICommand CancelClickedCommand { get; set; }

        public JobItem JobInfoItem { get; set; }
        private IEmailService _emailService;
        private ICapacityEngine _capacityEngine;
        public List<BuildingSystem> BuildingSystems { get; set; }

        public JobInfoPageViewModel(IEmailService emailService,
            ICapacityEngine capacityEngine, ICacheService cacheService) : base(cacheService)
        {
            _cacheService = cacheService;
            _emailService = emailService;
            _capacityEngine = capacityEngine;

            JobTypes = new List<string>()
            {
                "SF",
                "MF",
                "LA"
            };

            Phases = new ObservableCollection<Phase>();

            BuildingSystems = new List<BuildingSystem>();

            MessagingCenter.Subscribe<PhaseCreationPage>(this, "Phases Saved", (sender) =>
            {
                var creator = sender as PhaseCreationPage;
                Phases = creator.JobItem.Phases;
                OnPropertyChanged(nameof(Phases));
                if (Phases != null)
                    BuildingSystems = Phases.First().BuildingSystems;
                OnPropertyChanged(nameof(BuildingSystems));
            });

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
                         JobName = JobName,
                         DeliveryDate = DeliveryDate,
                         FloorSquareFeet = FloorSquareFeet,
                         HasWindows = HasWindows,
                         RoundTripMiles = RoundTripMiles,
                         Phases = Phases,
                         //SelectedBuildingSystem = SelectedBuildingSystem,
                         SelectedJobType = SelectedJobType,
                         //SelectedPhase = SelectedPhase,
                         WallBoardFeet = WallBoardFeet,
                         WindowDeliveryDate = WindowDeliveryDate,
                         WindowsInstalled = WindowsInstalled
                     };

                     await _cacheService.SaveJobItem(JobInfoItem);
                     var schedule = await _capacityEngine.CalculateSchedules(JobInfoItem);

                     await _cacheService.SaveJobSchedule(schedule);
                     await SendCompletionEmail();
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

        private async Task<bool> SendCompletionEmail()
        {
            string body = $"Sean Stilson has created a new {SelectedJobType} job for your review.";
            return await _emailService.SendEmail("New Job Created", body, new List<string>() { "scott.jeffers@blenkerco.com" });
        }


        public void TransferDataAndLeave()
        {
            JobInfoItem = new JobItem
            {
                DeliveryDate = DeliveryDate,
                FloorSquareFeet = FloorSquareFeet,
                HasWindows = HasWindows,
                RoundTripMiles = RoundTripMiles,
                //SelectedBuildingSystem = SelectedBuildingSystem,
                SelectedJobType = SelectedJobType,
                //SelectedPhase = SelectedPhase,
                WallBoardFeet = WallBoardFeet,
                WindowDeliveryDate = WindowDeliveryDate,
                WindowsInstalled = WindowsInstalled
            };

            App.Current.MainPage.Navigation.PopModalAsync();
        }
        public string JobName { get; set; }

        public List<String> JobTypes { get; set; }
        public string SelectedJobType { get; set; }

        public ObservableCollection<Phase> Phases { get; set; }
        public string SelectedPhase { get; set; }

        public string SelectedBuildingSystem { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int RoundTripMiles { get; set; }
        public string RountTripMilesString => RoundTripMiles.ToString();

        public bool HasWindows { get; set; }
        public bool WindowsInstalled { get; set; }

        public DateTime WindowDeliveryDate { get; set; }

        public Double WallBoardFeet => System.Convert.ToDouble(WallFeetString?.ToString());
        public string WallFeetString {get; set;}

        public Double FloorSquareFeet => System.Convert.ToDouble(FloorFeetString?.ToString());
        public string FloorFeetString {get; set;}

    }

}
