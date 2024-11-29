using System.Linq.Expressions;
using Core.AOP.Aspects;
using Core.Persistence.Extensions;
using Core.Security.Entities;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Entities;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Rules;

namespace TechCareer.Service.Concretes;
public sealed class VideoEducationService(IVideoEducationRepository _videoEducationRepository, VideoEducationBusinessRules _videoEducationBusinessRules) : IVideoEducationService
{
    public async Task<VideoEducation?> GetAsync(Expression<Func<VideoEducation, bool>> predicate, bool include = false, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var videoEducation = await _videoEducationRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return videoEducation;
    }


    //[CacheAspect(bypassCache:false, cacheKeyTemplate:"VideoEducations({index},{size})",cacheGroupKey:"VideoEducations",Priority = 3)]
    public async Task<Paginate<VideoEducation>> GetPaginateAsync(Expression<Func<VideoEducation, bool>>? predicate = null, Func<IQueryable<VideoEducation>, IOrderedQueryable<VideoEducation>>? orderBy = null, bool include = false, int index = 0,
        int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        Paginate<VideoEducation> videoEducationList = await _videoEducationRepository.GetPaginateAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return videoEducationList;
    }

    public async Task<List<VideoEducation>> GetListAsync(Expression<Func<VideoEducation, bool>>? predicate = null, Func<IQueryable<VideoEducation>, IOrderedQueryable<VideoEducation>>? orderBy = null, bool include = false, bool withDeleted = false,
        bool enableTracking = false, CancellationToken cancellationToken = default)
    {
        List<VideoEducation> videoEducationList = await _videoEducationRepository.GetListAsync(
            predicate, orderBy, include, withDeleted, enableTracking, cancellationToken
        );
        return videoEducationList;
    }

    public async Task<VideoEducation> AddAsync(VideoEducation videoEducation)
    {
        // TODO: video education business rules

        VideoEducation addedVideoEducation = await _videoEducationRepository.AddAsync(videoEducation);

        return addedVideoEducation;
    }

    public async Task<VideoEducation> UpdateAsync(VideoEducation videoEducation)
    {
        // TODO: video education business rules

        VideoEducation updatedVideoEducation = await _videoEducationRepository.UpdateAsync(videoEducation);

        return updatedVideoEducation;
    }

    public async Task<VideoEducation> DeleteAsync(VideoEducation videoEducation, bool permanent = false)
    {
        // TODO: video education business rules

        VideoEducation deletedVideoEducation = await _videoEducationRepository.DeleteAsync(videoEducation, permanent);

        return deletedVideoEducation;
    }
}