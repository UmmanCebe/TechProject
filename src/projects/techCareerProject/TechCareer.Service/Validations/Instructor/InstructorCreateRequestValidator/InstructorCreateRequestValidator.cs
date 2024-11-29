using FluentValidation;
using TechCareer.Models.Dtos.Instructor.Request;
using TechCareer.Models.Enums;

namespace TechCareer.Service.Validations.Instructor.InstructorCreateRequestValidator;
public class InstructorCreateRequestValidator : AbstractValidator<InstructorCreateRequestDto>
{
    public InstructorCreateRequestValidator()
    {
        RuleFor(i => i.Name).NotEmpty().WithMessage("Eğitmen adı alanı boş olamaz.");
        RuleFor(i => i.About).NotEmpty().WithMessage("Hakkında alanı boş olamaz")
            .Length(10, 150).WithMessage("Hakkında alanı minimum 10 maksimum 150 karakter olabilir.");
    }
}