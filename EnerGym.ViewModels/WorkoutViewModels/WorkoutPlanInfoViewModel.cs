using EnerGym.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerGym.ViewModels.WorkoutViewModels
{
    public class WorkoutPlanInfoViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}
