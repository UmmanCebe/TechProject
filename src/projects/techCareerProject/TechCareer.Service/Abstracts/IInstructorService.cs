using Core.Persistence.Extensions;
using System.Linq.Expressions;
using TechCareer.Models.Dtos.Instructors.Request;
using TechCareer.Models.Dtos.Instructors.Response;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Abstracts;
public interface IInstructorService
{
    Task<Instructor?> GetOneAsync(
        Expression<Func<Instructor, bool>> predicate,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<Paginate<Instructor>> GetPaginateAsync(
        Expression<Func<Instructor, bool>>? predicate = null,
        Func<IQueryable<Instructor>, IOrderedQueryable<Instructor>>? orderBy = null,
        bool include = false,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<List<Instructor>> GetAllAsync(
        Expression<Func<Instructor, bool>>? predicate = null,
        Func<IQueryable<Instructor>, IOrderedQueryable<Instructor>>? orderBy = null,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<Instructor> AddAsync(InstructorCreateRequestDto dto);
    Task<InstructorResponseDto> UpdateAsync(InstructorUpdateRequestDto dto,Guid id);
    Task<Instructor> DeleteAsync(Instructor ınstructor, bool permanent = false);
}