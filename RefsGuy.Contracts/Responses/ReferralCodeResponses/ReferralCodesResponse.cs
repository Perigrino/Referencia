namespace RefsGuy.Contracts.Responses.ReferralCodeResponses;

public class ReferralCodesResponse
{
    public required IEnumerable<ReferralCodeResponse> ReferralCode { get; init; } = Enumerable.Empty<ReferralCodeResponse>();
}