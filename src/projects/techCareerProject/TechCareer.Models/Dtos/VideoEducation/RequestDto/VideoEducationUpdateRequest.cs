using TechCareer.Models.Enums;

namespace TechCareer.Models.Dtos.VideoEducation.RequestDto;

public class VideoEducationUpdateRequest
{
    public string Title { get; init; }
    public string Description { get; init; }
    public double TotalHour { get; init; }
    public bool IsCertified { get; init; }
    public int Level { get; init; }
    public string ImageUrl { get; init; }
    public Guid InstructorId { get; init; }
    public string ProgrammingLanguage { get; init; }
}
