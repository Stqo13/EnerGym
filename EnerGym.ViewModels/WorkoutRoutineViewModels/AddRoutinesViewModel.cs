﻿namespace EnerGym.ViewModels.WorkoutRoutineViewModels
{
    public class AddRoutinesViewModel
    {
        public int WorkoutPlanId { get; set; }
        public IEnumerable<WorkoutRoutineSelectViewModel> AvailableRoutines { get; set; }
             = new List<WorkoutRoutineSelectViewModel>();
        public List<int> SelectedRoutineIds { get; set; } 
             = new List<int>();

        public string? SearchQuery { get; set; }

        public int? Sets { get; set; }

        public int? Reps { get; set; }
    }
}
