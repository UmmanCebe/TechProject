using Core.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechCareer.Models.Dtos.Categories.RequestDto;
using TechCareer.Models.Dtos.Categories.ResponseDto;

using TechCareer.Models.Entities;

namespace TechCareer.Service.Abstracts;

public interface ICategoryService
{

    Task<CategoryDto?> GetAsync(
            Expression<Func<Category, bool>> predicate,
            bool include = false,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        );

    Task<Paginate<CategoryDto>> GetPaginateAsync(
        Expression<Func<Category, bool>>? predicate = null,
        Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null,
        bool include = false,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<List<CategoryDto>> GetListAsync(
        Expression<Func<Category, bool>>? predicate = null,
        Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<CategoryDto> AddAsync(CreateCategoryRequestDto createCategoryRequestDto);
    Task<CategoryDto> UpdateAsync(int id, UpdateCategoryRequestDto updateCategoryRequestDto);
    Task<CategoryDto> DeleteAsync(int id, bool permanent = false);
}















