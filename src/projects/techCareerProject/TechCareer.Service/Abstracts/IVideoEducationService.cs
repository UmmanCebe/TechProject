using System.Linq.Expressions;
using Core.Persistence.Extensions;
using Core.Security.Entities;
using TechCareer.Models.Dtos.VideoEducation.RequestDto;
using TechCareer.Models.Dtos.VideoEducation.ResponseDto;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Abstracts;

public interface IVideoEducationService
{
    Task<VideoEducationResponse> GetAsync(
        Expression<Func<VideoEducation, bool>> predicate,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Task<Paginate<VideoEducationResponse>> GetPaginateAsync(
        Expression<Func<VideoEducation, bool>>? predicate = null,
        Func<IQueryable<VideoEducation>, IOrderedQueryable<VideoEducation>>? orderBy = null,
        bool include = false,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Task<List<VideoEducationResponse>> GetListAsync(
        Expression<Func<VideoEducation, bool>>? predicate = null,
        Func<IQueryable<VideoEducation>, IOrderedQueryable<VideoEducation>>? orderBy = null,
        bool include = false,
        bool withDeleted = false,
        bool enableTracking = false,
        CancellationToken cancellationToken = default);

    Task<VideoEducationResponse> AddAsync(VideoEducationCreateRequest request);
    Task<VideoEducationResponse> UpdateAsync(int id, VideoEducationUpdateRequest request);
    Task<VideoEducationResponse> DeleteAsync(int id, bool permanent = false);
}