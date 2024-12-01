using Core.Persistence.Repositories;
using TechCareer.DataAccess.Contexts;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Repositories.Concretes;

public class CategoryRepository(BaseDbContext context) : EfRepositoryBase<Category, int, BaseDbContext>(context), ICategoryRepository
{
}