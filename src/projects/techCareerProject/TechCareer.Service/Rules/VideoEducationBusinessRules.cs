using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.CrossCuttingConcerns.Rules;
using Core.Security.Entities;
using Core.Security.Hashing;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.DataAccess.Repositories.Concretes;
using TechCareer.Models.Entities;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Rules;

public sealed class VideoEducationBusinessRules(IVideoEducationRepository _videoEducationRepository) : BaseBusinessRules
{
    public Task VideoEducationShouldBeExistsWhenSelected(VideoEducation? videoEducation)
    {
        if (videoEducation == null)
            throw new BusinessException("Video education does not exist");
        return Task.CompletedTask;
    }

    public async Task VideoEducationIdShouldBeExistsWhenSelected(int id)
    {
        bool doesExist = await _videoEducationRepository.AnyAsync(predicate: u => u.Id == id, enableTracking: false);
        if (doesExist)
            throw new BusinessException("Video education does not exist");
    }
}
