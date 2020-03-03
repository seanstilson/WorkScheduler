using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;
using WorkScheduler.Services;
using Xamarin.Forms.Internals;

namespace WorkScheduler.ViewModels
{
    public class PhaseCreationPageViewModel : BasePageViewModel
    {
        private ObservableCollection<Phase> _phases;

        public ObservableCollection<Phase> Phases {
            get { return _phases; }
            set
            {
                _phases = value ?? new ObservableCollection<Phase>() { BuildSinglePhase() };
                OnPropertyChanged(nameof(Phases));
            }
        }

        public List<string> Systems { get; set; }

        public PhaseCreationPageViewModel(ICacheService cacheService) : base(cacheService)
        {
            Systems = new List<string>()
            {
                "Walls 1",
                "Walls 2",
                "Walls 3",
                "Floors 1",
                "Floors 2",
                "Floors 3",
                "Roof Truss 1",
                "Roof Truss 2",
                "Basement 1"
            };
        }

        public void AddNewBuildingSystem(string phaseName)
        {
            var phase = Phases.SingleOrDefault(p => p.PhaseName == phaseName);
            if ( phase != null)
            {
                BuildingSystem bs = new BuildingSystem
                {
                    FullName = "TBD",
                    ActualBoardFeet = 0,
                    Code = "TBD",
                    EstimatedBoardFeet = 0
                };
                phase.BuildingSystems.Add(bs);
            }
            var lister = new List<Phase>();
            Phases.ForEach(p => lister.Add(p));
            Phases.Clear();
            Phases = new ObservableCollection<Phase>(lister);
            OnPropertyChanged(nameof(Phases));
        }

        public void AddNewPhase()
        {
            int ix = Phases.Count + 1;
            var phase = new Phase
            {
                Index = ix,
                JobId = Phases.First().JobId,
            };

            if (phase != null)
            {
                BuildingSystem bs = new BuildingSystem
                {
                    FullName = "TBD",
                    ActualBoardFeet = 0,
                    Code = "TBD",
                    EstimatedBoardFeet = 0
                };
                phase.BuildingSystems = new List<BuildingSystem>() { bs };
            }
            var lister = new List<Phase>();
            Phases.ForEach(p => lister.Add(p));
            lister.Add(phase);
            Phases.Clear();
            Phases = new ObservableCollection<Phase>(lister);
            OnPropertyChanged(nameof(Phases));
        }

        private Phase BuildSinglePhase()
        {
            return new Phase
                {
                Index = 1,
                JobId = Guid.NewGuid(),
                BuildingSystems = new System.Collections.Generic.List<BuildingSystem>()
                {
                    new BuildingSystem
                    {
                        ActualBoardFeet = 2800,
                        Code = "W1",
                        EstimatedBoardFeet = 2600,
                        FullName = "Walls 1"
                    }
                }
            };
        }

        public async Task<bool> SaveJobItem(JobItem jobItem )
        {
            try
            {
                await _cacheService.SaveJobItem(jobItem);
                return true;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
