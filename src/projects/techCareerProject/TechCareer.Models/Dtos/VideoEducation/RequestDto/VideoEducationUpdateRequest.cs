using TechCareer.Models.Enums;

namespace TechCareer.Models.Dtos.VideoEducation.RequestDto;

public class VideoEducationUpdateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double TotalHour { get; set; }
    public bool IsCertified { get; set; }
    public Level Level { get; set; }
    public string ImageUrl { get; set; }
    public string ProgrammingLanguage { get; set; }
}
