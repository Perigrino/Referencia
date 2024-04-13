using Newtonsoft.Json;

namespace RefsGuy.Application.Models;

public class ReferralCode
{
    public Guid Id { get; set; }
    public string? Code { get;set; }
    public bool IsRedeemed { get; set; }
    public double DiscountAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    
    //Navigation Properties
    public Guid UserId { get; set; }
    [JsonIgnore] public Users? Users { get; set; }
}