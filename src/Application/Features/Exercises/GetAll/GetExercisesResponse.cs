using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Exercises.GetAll
{
    public class GetExercisesResponse
    {
        public IEnumerable<ExerciseDto> ExerciseDtos { get; set; } = [];
    }
}
