using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Workouts.Create
{
    public class CreateWorkoutCommand
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
