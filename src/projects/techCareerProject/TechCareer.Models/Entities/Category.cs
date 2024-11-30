using Core.Persistence.Repositories.Entities;

namespace TechCareer.Models.Entities;
public class Category : Entity<int>
{
    public string Name { get; set; }
    public ICollection<Event> Events { get; set; }
}