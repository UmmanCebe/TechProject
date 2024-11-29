using FluentValidation;
using TechCareer.Models.Dtos.VideoEducation.RequestDto;
using TechCareer.Models.Enums;

namespace TechCareer.Service.Validations.Users;

public class VideoEducationCreateRequestValidator : AbstractValidator<VideoEducationCreateRequest>
{
    public VideoEducationCreateRequestValidator()
    {
        RuleFor(ve => ve.Title).NotEmpty().WithMessage("Video eğitimi başlığı boş olamaz.");
        RuleFor(ve => ve.Description).NotEmpty().WithMessage("Video eğitimi tanımı boş olamaz.");
        RuleFor(ve => ve.TotalHour).GreaterThanOrEqualTo(0.0).WithMessage("Video eğitimi süresi negatif sayı olamaz.");
        RuleFor(ve => ve.IsCertified).NotNull().WithMessage("Video eğitimi sertifikalı olma durumu boş olamaz.");
        RuleFor(ve => ve.Level).Must(level => Enum.IsDefined(typeof(Level), level))
                               .WithMessage("Video eğitimi seviyesi geçerli bir seviye olmalıdır.");
        RuleFor(ve => ve.ImageUrl).NotEmpty().WithMessage("Video eğitimi Image URL boş olamaz.");
        RuleFor(ve => ve.ProgrammingLanguage).NotEmpty().WithMessage("Video eğitimi programlama dili boş olamaz.");
    }
}
