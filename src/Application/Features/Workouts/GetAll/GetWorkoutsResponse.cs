using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Workouts.GetAll
{
    public class GetWorkoutsResponse
    {
        public List<WorkoutDto> Workouts { get; set; } = [];
    }
}
