using AutoMapper;
using Core.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.Category.RequestDto;
using TechCareer.Models.Dtos.Category.ResponseDto;
using TechCareer.Models.Entities;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Rules;

namespace TechCareer.Service.Concretes;

public sealed class CategoryService(ICategoryRepository _categoryRepository,
                                      CategoryBusinessRules _categoryBusinessRules,
                                      IMapper _mapper) : ICategoryService
{
    public async Task<CategoryDto?> GetAsync(
        Expression<Func<Category, bool>> predicate,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);

        // Map Category to CategoryDto
        return category is not null ? _mapper.Map<CategoryDto>(category) : null;
    }

    public async Task<Paginate<CategoryDto>> GetPaginateAsync(
        Expression<Func<Category, bool>>? predicate = null,
        Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null,
        bool include = false,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetPaginateAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );

        // Map Paginate<Category> to Paginate<CategoryDto>
        return _mapper.Map<Paginate<CategoryDto>>(categories);
    }

    public async Task<List<CategoryDto>> GetListAsync(
        Expression<Func<Category, bool>>? predicate = null,
        Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );

        // Map List<Category> to List<CategoryDto>
        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> AddAsync(CreateCategoryRequestDto createCategoryRequestDto)
    {
        // Validate business rules
        await _categoryBusinessRules.CategoryNameShouldNotExistWhenInsert(createCategoryRequestDto.Name);

        // Map CreateCategoryRequestDto to Category
        var category = _mapper.Map<Category>(createCategoryRequestDto);

        var addedCategory = await _categoryRepository.AddAsync(category);

        // Map added Category to CategoryDto
        return _mapper.Map<CategoryDto>(addedCategory);
    }

    public async Task<CategoryDto> UpdateAsync(UpdateCategoryRequestDto updateCategoryRequestDto)
    {
        // Validate business rules
        await _categoryBusinessRules.CategoryNameShouldNotExistWhenUpdate(updateCategoryRequestDto.Id, updateCategoryRequestDto.Name);

        // Map UpdateCategoryRequestDto to Category
        var category = _mapper.Map<Category>(updateCategoryRequestDto);

        var updatedCategory = await _categoryRepository.UpdateAsync(category);

        // Map updated Category to CategoryDto
        return _mapper.Map<CategoryDto>(updatedCategory);
    }

    public async Task<CategoryDto> DeleteAsync(int id, bool permanent = false)
    {
        // Retrieve category to delete
        var category = await _categoryRepository.GetAsync(c => c.Id == id);

        // Validate business rules
        await _categoryBusinessRules.CategoryShouldExistWhenSelected(category);

        var deletedCategory = await _categoryRepository.DeleteAsync(category!, permanent);

        // Map deleted Category to CategoryDto
        return _mapper.Map<CategoryDto>(deletedCategory);
    }
}