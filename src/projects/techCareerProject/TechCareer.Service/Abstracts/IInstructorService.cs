using Core.Persistence.Extensions;
using System.Linq.Expressions;
using TechCareer.Models.Dtos.Instructors.Request;
using TechCareer.Models.Dtos.Instructors.Response;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Abstracts;
public interface IInstructorService
{
    Task<InstructorResponseDto?> GetOneAsync(
        Expression<Func<Instructor, bool>> predicate,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<Paginate<InstructorResponseDto>> GetPaginateAsync(
        Expression<Func<Instructor, bool>>? predicate = null,
        Func<IQueryable<Instructor>, IOrderedQueryable<Instructor>>? orderBy = null,
        bool include = false,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<List<InstructorResponseDto>> GetAllAsync(
        Expression<Func<Instructor, bool>>? predicate = null,
        Func<IQueryable<Instructor>, IOrderedQueryable<Instructor>>? orderBy = null,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<InstructorResponseDto> AddAsync(InstructorCreateRequestDto dto);
    Task<InstructorResponseDto> UpdateAsync(InstructorUpdateRequestDto dto,Guid id);
    Task<InstructorResponseDto> DeleteAsync(Guid id, bool permanent = false);
}