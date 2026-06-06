using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Workouts.GetAll
{
    public class GetWorkoutsResult
    {
        public List<Workout> Workouts { get; set; } = [];
    }
}
