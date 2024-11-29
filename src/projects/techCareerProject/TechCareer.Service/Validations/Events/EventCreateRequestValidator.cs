using FluentValidation;
using TechCareer.Models.Dtos.Events.Request;

namespace TechCareer.Service.Validations.Events;

public class EventCreateRequestValidator : AbstractValidator<EventCreateRequestDto>
{
    public EventCreateRequestValidator()
    {

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Etkinlik başlığı boş olamaz.")
            .MinimumLength(3).WithMessage("Etkinlik başlığı en az 3 karakter olmalıdır.")
            .MaximumLength(100).WithMessage("Etkinlik başlığı en fazla 100 karakter olabilir.");


        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Etkinlik açıklaması boş olamaz.")
            .MinimumLength(10).WithMessage("Etkinlik açıklaması en az 10 karakter olmalıdır.")
            .MaximumLength(1000).WithMessage("Etkinlik açıklaması en fazla 1000 karakter olabilir.");


        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Etkinlik görsel URL'si boş olamaz.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("Geçerli bir URL formatı giriniz.");


        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Etkinlik başlangıç tarihi boş olamaz.")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Başlangıç tarihi bugünden sonraki bir tarih olmalıdır.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("Etkinlik bitiş tarihi boş olamaz.")
            .GreaterThan(x => x.StartDate).WithMessage("Bitiş tarihi, başlangıç tarihinden sonra olmalıdır.");


        RuleFor(x => x.ApplicationDeadline)
            .NotEmpty().WithMessage("Başvuru son tarihi boş olamaz.")
            .LessThan(x => x.StartDate).WithMessage("Başvuru son tarihi, başlangıç tarihinden önce olmalıdır.");


        RuleFor(x => x.ParticipationText)
            .NotEmpty().WithMessage("Katılım bilgisi boş olamaz.")
            .MaximumLength(500).WithMessage("Katılım bilgisi en fazla 500 karakter olabilir.");


        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Kategori kimliği pozitif bir sayı olmalıdır.");
    }
}
