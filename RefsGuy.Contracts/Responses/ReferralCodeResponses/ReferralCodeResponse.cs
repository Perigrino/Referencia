namespace RefsGuy.Contracts.Responses.ReferralCodeResponses;

public class ReferralCodeResponse
{
    public Guid Id { get; set; }
    public string? Code { get;set; }
    public bool IsRedeemed { get; set; }
    public double DiscountAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public Guid UserId { get; set; }
}