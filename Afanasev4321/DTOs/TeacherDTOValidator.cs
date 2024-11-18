using FluentValidation;
using Afanasev4321.DTOs;

public class TeacherDTOValidator : AbstractValidator<TeacherDTO>
{
    public TeacherDTOValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.DepartmentId).GreaterThan(0).WithMessage("Valid DepartmentId is required.");
        RuleFor(x => x.Degree).NotEmpty().WithMessage("Degree is required.");
        RuleFor(x => x.Position).NotEmpty().WithMessage("Position is required.");
    }
}
