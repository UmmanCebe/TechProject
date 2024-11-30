using FluentValidation;
using TechCareer.Models.Dtos.Instructors.Request;
using TechCareer.Models.Enums;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Validations.Instructor.InstructorCreateRequestValidator;
public class InstructorCreateRequestValidator : AbstractValidator<InstructorCreateRequestDto>
{
    public InstructorCreateRequestValidator()
    {
        RuleFor(i => i.Name).NotEmpty().WithMessage(InstructorMessages.InstructorNameNotBeEmpty);
        RuleFor(i => i.About).NotEmpty().WithMessage(InstructorMessages.InstructorAboutFieldNotBeEmpty)
            .Length(10, 150).WithMessage(InstructorMessages.InstructorAboutFieldLength);
    }
}