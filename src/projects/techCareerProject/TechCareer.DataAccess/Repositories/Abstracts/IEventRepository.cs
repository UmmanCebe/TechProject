using Core.Persistence.Repositories;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Repositories.Abstracts;

public interface IEventRepository : IAsyncRepository<Event, Guid>
{

}
