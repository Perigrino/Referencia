using RefsGuy.Application.Models;
using RefsGuy.Contracts.Requests.ReferralCodeRequests;
using RefsGuy.Contracts.Responses.ReferralCodeResponses;

namespace RefsGuy.API.ContractMappings;

public static class ReferralCodeContractMapping
{
    public static ReferralCode MapToReferralCode(this CreateReferralCodeRequest request)  //This maps the CreateReferralCodeDto to ReferralCode
    {
        return new ReferralCode
        {
            Id = Guid.NewGuid(),
            // Code = request.Code,
            // IsRedeemed = request.IsRedeemed,
            // DiscountAmount = request.DiscountAmount,
            // CreatedAt = request.CreatedAt,
            // ExpiresAt = request.ExpiresAt,
            UserId = request.UsersId
        };
    
    }
    
    // public static ReferralCode MapToReferralCode(this UpdateReferralCodeRequest request, Guid id)  //This maps the UpdateReferralCodeDto to ReferralCode
    // {
    //     return new ReferralCode
    //     {
    //         Id = Guid.NewGuid(),
    //         Code = request.Code,
    //         IsRedeemed = request.IsRedeemed,
    //         DiscountAmount = request.DiscountAmount,
    //         CreatedAt = request.CreatedAt,
    //         ExpiresAt = request.ExpiresAt,
    //         UserId = request.UsersId
    //     };
    //
    // }
    
    public static ReferralCodeResponse MapsToResponse(this ReferralCode referralCode) //This maps the ReferralCode to ReferralCodesResponse Dto
    {
        return new ReferralCodeResponse()
        {
            Id = referralCode.Id,
            Code = referralCode.Code,
            IsRedeemed = referralCode.IsRedeemed,
            DiscountAmount = referralCode.DiscountAmount,
            CreatedAt = referralCode.CreatedAt,
            ExpiresAt = referralCode.ExpiresAt,
            UserId = referralCode.UserId,
        };
    }
    
    public static ReferralCodesResponse MapsToResponse(this IEnumerable<ReferralCode> referralCodes) //This maps the list of referral codes to the ReferralCodeResponses
    {
        return new ReferralCodesResponse()
        {
            ReferralCode = referralCodes.Select(MapsToResponse)
        };
    }
}