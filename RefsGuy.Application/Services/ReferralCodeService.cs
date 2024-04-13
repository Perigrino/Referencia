using RefsGuy.Application.Interfaces;
using RefsGuy.Application.Models;

namespace RefsGuy.Application.Services;

public class ReferralCodeService : IReferralCodeService
{
    private readonly IUserRepository _userRepository;
    private readonly IReferralCodeRepository _referralCodeRepository;

    public ReferralCodeService(IUserRepository userRepository, IReferralCodeRepository referralCodeRepository)
    {
        _userRepository = userRepository;
        _referralCodeRepository = referralCodeRepository;
    }

    public bool HasReachedMaxReferralCodes(Guid userId, CancellationToken token = default)
    {
        var user = _userRepository.GetUsersById(userId, token);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var numberOfReferralCode = user.Result.ReferralCode.Count;
        
        return numberOfReferralCode >= 5;
    }

    public bool HasReferralCodeExpired(ReferralCode referralCode, CancellationToken token = default)
    {
        var expirationDate = referralCode.ExpiresAt;
        return DateTime.Now > expirationDate;
    }

    public bool HasReferralCodeBeenRedeemed(ReferralCode referralCode, CancellationToken token = default)
    {
        var redeemCode = referralCode.IsRedeemed;
        return redeemCode;
    }
    
    
    public bool ReferralCodeExists(Guid userId, string? referralCode, CancellationToken token = default)
    {
        var result = _userRepository.GetUsersById(userId);
        if (result == null)
        {
            var walletExists = result?.Result.ReferralCode;
            if (walletExists != null)
            {
                var wallet = walletExists.FirstOrDefault(rf => rf.Code == referralCode);
            }
            return true;
        }

        return false;
    }
}