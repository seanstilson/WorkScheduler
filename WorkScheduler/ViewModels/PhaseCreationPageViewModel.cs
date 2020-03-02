using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WorkScheduler.Models;
using WorkScheduler.Services;
using Xamarin.Forms.Internals;

namespace WorkScheduler.ViewModels
{
    public class PhaseCreationPageViewModel : BasePageViewModel
    {
        public ObservableCollection<Phase> Phases { get; set; }

        public List<string> Systems { get; set; }

        public PhaseCreationPageViewModel(ICacheService cacheService) : base(cacheService)
        {
            Phases = new ObservableCollection<Phase>()
            {
                new Phase
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
                        },
                        new BuildingSystem
                        {
                            ActualBoardFeet = 3400,
                            Code = "W2",
                            EstimatedBoardFeet = 3800,
                            FullName = "Walls 2"
                        }
                    }
                }
            };
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
    }
}
