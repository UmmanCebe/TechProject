

namespace TechCareer.Models.Dtos.Events.Request;



public sealed record EventCreateRequestDto
{
    public string Title { get; init; }
    public string Description { get; init; }
    public string ImageUrl { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public DateTime ApplicationDeadline { get; init; }
    public string ParticipationText { get; init; }
    public int CategoryId { get; init; }
}

