using Core.Persistence.Repositories;
using TechCareer.DataAccess.Contexts;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Repositories.Concretes;

public class VideoEducationRepository(BaseDbContext context) : EfRepositoryBase<VideoEducation, int, BaseDbContext>(context), IVideoEducationRepository
{
}
