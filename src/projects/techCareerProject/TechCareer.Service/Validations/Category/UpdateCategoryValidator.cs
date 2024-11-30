using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareer.Models.Dtos.Categories.RequestDto;

namespace TechCareer.Service.Validations.Category;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequestDto>
{

    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Kategori adı boş olamaz.")
            .Length(3, 50).WithMessage("Kategori adı 3 ile 50 karakter arasında olmalıdır.");







    }
}