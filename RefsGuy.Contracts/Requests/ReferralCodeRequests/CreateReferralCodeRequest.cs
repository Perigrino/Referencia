namespace RefsGuy.Contracts.Requests.ReferralCodeRequests;

public class CreateReferralCodeRequest
{
    // public string? Code { get; set; }
    // public bool IsRedeemed { get; set; }
    // public double DiscountAmount { get; set; }
    // public DateTime CreatedAt { get; set; }
    // public DateTime? ExpiresAt { get; set; }
    public Guid UsersId { get; set; }
}