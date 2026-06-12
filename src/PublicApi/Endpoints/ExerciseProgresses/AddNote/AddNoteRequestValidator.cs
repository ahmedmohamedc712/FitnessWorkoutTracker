using System.ComponentModel.DataAnnotations;
using Application.Features.ExerciseProgresses.AddNote;
using FastEndpoints;
using FluentValidation;

namespace PublicApi.Endpoints.ExerciseProgresses.AddNote;

public class AddNoteRequestValidator : Validator<AddNoteRequest>
{
    public AddNoteRequestValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("Note cannot be empty.");
    }
}
