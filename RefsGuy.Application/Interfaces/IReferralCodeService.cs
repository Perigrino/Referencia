using RefsGuy.Application.Models;

namespace RefsGuy.Application.Interfaces;

public interface IReferralCodeService
{
    bool HasReachedMaxReferralCodes(Guid userId, CancellationToken token = default);
    public bool ReferralCodeExists(Guid userId, string? referralCode, CancellationToken token = default);
    bool HasReferralCodeExpired(ReferralCode referralCode, CancellationToken token = default);
    bool HasReferralCodeBeenRedeemed(ReferralCode referralCode, CancellationToken token = default);
}