using System.Linq.Expressions;
using AutoMapper;
using Core.AOP.Aspects;
using Core.Persistence.Extensions;
using Core.Security.Entities;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.VideoEducation.RequestDto;
using TechCareer.Models.Dtos.VideoEducation.ResponseDto;
using TechCareer.Models.Entities;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Rules;

namespace TechCareer.Service.Concretes;
public class VideoEducationService(
    IVideoEducationRepository _videoEducationRepository,
    VideoEducationBusinessRules _videoEducationBusinessRules,
    IMapper mapper) : IVideoEducationService
{
    public async Task<VideoEducationResponse> GetAsync(Expression<Func<VideoEducation, bool>> predicate, bool include = false, bool withDeleted = false, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        VideoEducation? videoEducation = await _videoEducationRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);

        VideoEducationResponse response = mapper.Map<VideoEducationResponse>(videoEducation);

        return response;
    }


    //[CacheAspect(cacheKeyTemplate: "VideoEducations({page},{size})", bypassCache: false, cacheGroupKey: "VideoEducations")]
    public async Task<Paginate<VideoEducationResponse>> GetPaginateAsync(Expression<Func<VideoEducation, bool>>? predicate = null, Func<IQueryable<VideoEducation>, IOrderedQueryable<VideoEducation>>? orderBy = null, bool include = false, int index = 0,
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

        Paginate<VideoEducationResponse> response = mapper.Map<Paginate<VideoEducationResponse>>(videoEducationList);
        return response;
    }

    //[CacheAspect(cacheKeyTemplate: "VideoEducationList", bypassCache: false, cacheGroupKey: "VideoEducations")]
    public async Task<List<VideoEducationResponse>> GetListAsync(Expression<Func<VideoEducation, bool>>? predicate = null, Func<IQueryable<VideoEducation>, IOrderedQueryable<VideoEducation>>? orderBy = null, bool include = false, bool withDeleted = false,
        bool enableTracking = false, CancellationToken cancellationToken = default)
    {
        List<VideoEducation> videoEducationList = await _videoEducationRepository.GetListAsync(
            predicate, orderBy, include, withDeleted, enableTracking, cancellationToken
        );

        List<VideoEducationResponse> response = mapper.Map<List<VideoEducationResponse>>(videoEducationList);

        return response;
    }

    [LoggerAspect]
    //[ClearCacheAspect("VideoEducations")]
    [AuthorizeAspect("Admin")]
    public async Task<VideoEducationResponse> AddAsync(VideoEducationCreateRequest request)
    {
        VideoEducation videoEducation = mapper.Map<VideoEducation>(request);

        VideoEducation addedVideoEducation = await _videoEducationRepository.AddAsync(videoEducation);

        VideoEducationResponse response = mapper.Map<VideoEducationResponse>(addedVideoEducation);

        return response;
    }

    [LoggerAspect]
    //[ClearCacheAspect("VideoEducations")]
    [AuthorizeAspect("Admin")]
    public async Task<VideoEducationResponse> UpdateAsync(int id, VideoEducationUpdateRequest request)
    {
        await _videoEducationBusinessRules.VideoEducationIdShouldBeExistsWhenSelected(id);

        VideoEducation videoEducation = (await _videoEducationRepository.GetAsync(u => u.Id == id))!;

        videoEducation = mapper.Map(request, videoEducation);

        videoEducation.Id = id;

        VideoEducation updatedVideoEducation = await _videoEducationRepository.UpdateAsync(videoEducation);

        VideoEducationResponse response = mapper.Map<VideoEducationResponse>(updatedVideoEducation);

        return response;
    }

    [LoggerAspect]
    //[ClearCacheAspect("VideoEducations")]
    [AuthorizeAspect("Admin")]
    public async Task<VideoEducationResponse> DeleteAsync(int id, bool permanent = false)
    {
        await _videoEducationBusinessRules.VideoEducationIdShouldBeExistsWhenSelected(id);

        VideoEducation videoEducationToBeDeleted = (await _videoEducationRepository.GetAsync(u => u.Id == id))!;

        VideoEducation deletedVideoEducation = await _videoEducationRepository.DeleteAsync(videoEducationToBeDeleted, permanent);

        VideoEducationResponse response = mapper.Map<VideoEducationResponse>(deletedVideoEducation);

        return response;
    }
}