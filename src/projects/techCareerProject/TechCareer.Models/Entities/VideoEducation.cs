using Core.Persistence.Repositories.Entities;
using TechCareer.Models.Enums;

namespace TechCareer.Models.Entities;
public class VideoEducation : Entity<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double TotalHour { get; set; }
    public bool IsCertified { get; set; }
    public Level Level { get; set; }
    public string ImageUrl { get; set; }
    public Guid InstructorId { get; set; }
    public string ProgrammingLanguage { get; set; }
}