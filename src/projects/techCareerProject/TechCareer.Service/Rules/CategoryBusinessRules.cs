using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.CrossCuttingConcerns.Rules;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.DataAccess.Repositories.Concretes;
using TechCareer.Models.Entities;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Rules;

public class CategoryBusinessRules(ICategoryRepository _categoryRepository) : BaseBusinessRules
{

    public async virtual Task CategoryNameShouldNotExistWhenInsert(string name)
    {
        bool doesExist = await _categoryRepository.AnyAsync(predicate: c => c.Name == name, enableTracking: false);
        if (doesExist)
            throw new BusinessException(CategoryMessages.CategoryNameAlreadyExists);
    }

    public async virtual Task CategoryNameShouldNotExistWhenUpdate(int id, string name)
    {
        bool doesExist = await _categoryRepository.AnyAsync(predicate: c => c.Id != id && c.Name == name, enableTracking: false);
        if (doesExist)
            throw new BusinessException(CategoryMessages.CategoryNameAlreadyExists);
    }


    public async virtual Task CategoryShouldExistWhenSelected(Category? category)
    {
        if (category == null)
            throw new BusinessException(CategoryMessages.CategoryNameDoesNotExists);
    }

    public async virtual Task CategoryIdShouldBeExistsWhenSelected(int id)
    {
        bool doesExist = await _categoryRepository.AnyAsync(predicate: u => u.Id == id, enableTracking: false);
        if (doesExist is false)
            throw new BusinessException(VideoEducationMessages.DontExists);
    }

}
