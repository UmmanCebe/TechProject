using Core.Persistence.Repositories;
using TechCareer.DataAccess.Contexts;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Repositories.Concretes;

public sealed class InstructorRepository(BaseDbContext context) : EfRepositoryBase<Instructor, Guid, BaseDbContext>(context), IInstructorRepository
{

}
