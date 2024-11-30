using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.CrossCuttingConcerns.Rules;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Entities;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Rules;

public class VideoEducationBusinessRules(IVideoEducationRepository _videoEducationRepository) : BaseBusinessRules
{
    public virtual Task VideoEducationShouldBeExistsWhenSelected(VideoEducation? videoEducation)
    {
        if (videoEducation == null)
            throw new BusinessException(VideoEducationMessages.DontExists);
        return Task.CompletedTask;
    }

    public async virtual Task VideoEducationIdShouldBeExistsWhenSelected(int id)
    {
        bool doesExist = await _videoEducationRepository.AnyAsync(predicate: u => u.Id == id, enableTracking: false);
        if (doesExist is false)
            throw new BusinessException(VideoEducationMessages.DontExists);
    }
}
