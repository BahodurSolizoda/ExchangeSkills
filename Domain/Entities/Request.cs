using Domain.Enums;

namespace Domain.Entities;

public class Request
{
    public int RequestId { get; set; }
    public int FromUserId { get; set; }
    public int ToUserId { get; set; }
    public int RequestedSkillId { get; set; }
    public int OfferedSkillId { get; set; }
    public RequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}