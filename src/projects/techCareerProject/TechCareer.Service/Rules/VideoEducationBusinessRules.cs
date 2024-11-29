using Core.CrossCuttingConcerns.Rules;
using TechCareer.DataAccess.Repositories.Abstracts;

namespace TechCareer.Service.Rules;

public sealed class VideoEducationBusinessRules(IVideoEducationRepository _videoEducationRepository) : BaseBusinessRules
{

}
