using FluentValidation;
using TechCareer.Models.Dtos.Instructor.Request;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Validations.Instructor.InstructorUpdateRequestValidator;
public class InstructorUpdateRequestValidator : AbstractValidator<InstructorUpdateRequestDto>
{
    public InstructorUpdateRequestValidator()
    {
        RuleFor(i => i.Name).NotEmpty().WithMessage(InstructorMessages.InstructorNameNotBeEmpty);
        RuleFor(i => i.About).NotEmpty().WithMessage(InstructorMessages.InstructorAboutFieldNotBeEmpty)
            .Length(10, 150).WithMessage(InstructorMessages.InstructorAboutFieldLength);
    }
}