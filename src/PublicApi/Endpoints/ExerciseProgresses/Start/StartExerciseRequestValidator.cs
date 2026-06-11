using System.ComponentModel.DataAnnotations;
using Application.Features.ExerciseProgresses.Start;
using FastEndpoints;
using FluentValidation;

namespace PublicApi.Endpoints.ExerciseProgresses.Start;

public class StartExerciseRequestValidator : Validator<StartExerciseRequest>
{
    public StartExerciseRequestValidator()
    {
        RuleFor(x => x.Sets)
            .GreaterThan(0).WithMessage("Sets cannot be zero or negative.");
        
        RuleFor(x => x.Reps)
            .GreaterThan(0).WithMessage("Reps cannot be zero or negative.");
    }
}
