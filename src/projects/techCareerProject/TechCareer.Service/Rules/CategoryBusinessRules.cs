using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.CrossCuttingConcerns.Rules;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Entities;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Rules;

public sealed class CategoryBusinessRules(ICategoryRepository _categoryRepository) : BaseBusinessRules
{

    public async Task CategoryNameShouldNotExistWhenInsert(string name)
    {
        bool doesExist = await _categoryRepository.AnyAsync(predicate: c => c.Name == name, enableTracking: false);
        if (doesExist)
            throw new BusinessException(CategoryMessages.CategoryNameAlreadyExists);
    }

    public async Task CategoryNameShouldNotExistWhenUpdate(int id, string name)
    {
        bool doesExist = await _categoryRepository.AnyAsync(predicate: c => c.Id != id && c.Name == name, enableTracking: false);
        if (doesExist)
            throw new BusinessException(CategoryMessages.CategoryNameAlreadyExists);
    }


    public async Task CategoryShouldExistWhenSelected(Category? category)
    {
        if (category == null)
            throw new BusinessException(CategoryMessages.CategoryNameDoesNotExists);
    }

}
