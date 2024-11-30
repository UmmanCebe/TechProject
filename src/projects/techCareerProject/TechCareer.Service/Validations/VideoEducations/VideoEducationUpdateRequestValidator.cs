using FluentValidation;
using TechCareer.Models.Dtos.VideoEducation.RequestDto;
using TechCareer.Models.Enums;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Validations.Users;

public class VideoEducationUpdateRequestValidator : AbstractValidator<VideoEducationUpdateRequest>
{
    public VideoEducationUpdateRequestValidator()
    {
        RuleFor(ve => ve.Title).NotEmpty().WithMessage(VideoEducationMessages.TitleCannotBeEmpty);
        RuleFor(ve => ve.Description).NotEmpty().WithMessage(VideoEducationMessages.DescriptionCannotBeEmpty);
        RuleFor(ve => ve.TotalHour).GreaterThanOrEqualTo(0.0).WithMessage(VideoEducationMessages.TotalHourCannotBeNegative);
        RuleFor(ve => ve.IsCertified).NotNull().WithMessage(VideoEducationMessages.IsCertifiedIsRequired);
        RuleFor(ve => ve.Level).Must(level => Enum.IsDefined(typeof(Level), level))
                               .WithMessage(VideoEducationMessages.LevelMustBeValid);
        RuleFor(ve => ve.ImageUrl).NotEmpty().WithMessage(VideoEducationMessages.ImageUrlCannotBeEmpty);
        RuleFor(ve => ve.ProgrammingLanguage).NotEmpty().WithMessage(VideoEducationMessages.ProgrammingLanguageCannotBeEmpty);
    }
}