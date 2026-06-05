using Application.Features.Workouts.Create;
using FastEndpoints;
using FluentValidation;

namespace PublicApi.Endpoints.Workouts.Create
{
    public class CreateWorkoutValidator : Validator<CreateWorkoutCommand>
    {
        public CreateWorkoutValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Maximum title is 50 characters.");
        }
    }
}
