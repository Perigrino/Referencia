using Microsoft.AspNetCore.Mvc;
using RefsGuy.API.ContractMappings;
using RefsGuy.Application.Interfaces;
using RefsGuy.Contracts.Requests.ReferralCodeRequests;
using RefsGuy.Contracts.Responses;
using RefsGuy.Contracts.Responses.ReferralCodeResponses;

namespace RefsGuy.API.Controller;

[ApiController]
public class ReferralCodeController(IReferralCodeRepository referralCodeRepository, IReferralCodeService referralCodeService)
    : ControllerBase
{
    //GET all Referral Codes
    [HttpGet(ApiEndpoints.ReferralCode.GetAll)]
    public async Task<IActionResult> GetUsers(CancellationToken token)
    {
        var user = await referralCodeRepository.GetReferralCodeAsync(token);
        var userResponse = new FinalResponse<ReferralCodesResponse>
        {
            StatusCode = 200,
            Message = "Referral Codes retrieved successfully.",
            Data = user.MapsToResponse()
        };
        return Ok(userResponse);
    }
    
    //GET ReferralCodeByReferralCodeId
    [HttpGet(ApiEndpoints.ReferralCode.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
    {
        var referralCode = await referralCodeRepository.GetReferralCodeByReferralCodeId(id, token);
        if (referralCode == null)
        {
            return NotFound(new FinalResponse<object>
            {
                StatusCode = 404,
                Message = "Referral Code not found."
            });
        }
        
        var referralCodeResponse = new FinalResponse<ReferralCodeResponse>
        {
            StatusCode = 200,
            Message = "Referral Code retrieved successfully.",
            Data = referralCode.MapsToResponse()
        };
        return Ok(referralCodeResponse);
    }
    
    //POST Referral Code
    [HttpPost(ApiEndpoints.ReferralCode.Create)]
    public async Task<IActionResult> CreateReferralCode([FromBody] CreateReferralCodeRequest? request, CancellationToken token)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "Referral Code data is invalid." });
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }

        var hasReachedMaxReferralCodes = referralCodeService.HasReachedMaxReferralCodes(request.UsersId, token);
        if (!hasReachedMaxReferralCodes)
        {
            var mapToReferralCode = request.MapToReferralCode();
            await referralCodeRepository.CreateReferralCode(mapToReferralCode ?? throw new InvalidOperationException(), token);
            var referralCodeResponse = new FinalResponse<ReferralCodeResponse>
            {
                StatusCode = 201,
                Message = "Referral Code created successfully.",
                Data = mapToReferralCode.MapsToResponse()
            };
            return CreatedAtAction(nameof(Get), new { id = mapToReferralCode.Id }, referralCodeResponse);
        }
        
        var referralCodeMaxedResponse = new FinalResponse<object>
        {
            StatusCode = 400,
            Message = "User already has 5 active referral codes on account.",
            Data = null
        };
        return BadRequest(referralCodeMaxedResponse);
    }
    
    //DELETE Referral Code
    [HttpDelete(ApiEndpoints.ReferralCode.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await referralCodeRepository.ReferralCodeExistsId(id, token);
        var deleteReferralCode = await referralCodeRepository.DeleteReferralCode(id, token);
        if (!deleteReferralCode)
        {
            return NotFound(new FinalResponse<string>
            {
                StatusCode = 404,
                Message = "Referral Code not found or already deleted",
                Data = null
            });
        }
        
        return Ok(new FinalResponse<string>
        {
            StatusCode = 200,
            Message = "Referral code deleted successfully",
            Data = null
        });
    }
}