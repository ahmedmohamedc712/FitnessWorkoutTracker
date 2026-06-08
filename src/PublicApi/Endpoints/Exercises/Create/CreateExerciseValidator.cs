using Application.Features.Exercises.Create;
using FastEndpoints;
using FluentValidation;

namespace PublicApi.Endpoints.Exercises.Create
{
    public class CreateExerciseValidator : Validator<CreateExerciseRequest>
    {
        public CreateExerciseValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Maximum title is 50 characters.");
        }
    }
}
