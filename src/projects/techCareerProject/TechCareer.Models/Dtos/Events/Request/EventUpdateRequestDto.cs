using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCareer.Models.Dtos.Events.Request;

public sealed  record EventUpdateRequestDto
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
