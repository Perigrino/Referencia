using RefsGuy.Application.Models;

namespace RefsGuy.Application.Interfaces;

public interface IReferralCodeRepository
{
    Task<IEnumerable<ReferralCode>> GetReferralCodeAsync(CancellationToken token = default);
    Task<ReferralCode> GetReferralCodeByReferralCodeId(Guid referralCodeId, CancellationToken token = default);
    Task<bool> CreateReferralCode(ReferralCode referralCode , CancellationToken token = default);
    Task<bool> DeleteReferralCode(Guid referralCodeId, CancellationToken token = default);
    Task<bool> ReferralCodeExistsId(Guid referralCodeId, CancellationToken token = default);
    Task<bool> Save(CancellationToken token = default);
}