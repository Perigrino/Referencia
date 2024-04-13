using Microsoft.EntityFrameworkCore;
using RefsGuy.Application.Database;
using RefsGuy.Application.Interfaces;
using RefsGuy.Application.Models;

namespace RefsGuy.Application.Repository;

public class ReferralCodeRepository : IReferralCodeRepository
{
    private readonly AppDbContext _context;

    public ReferralCodeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReferralCode>> GetReferralCodeAsync(CancellationToken token = default)
    {
        var result = await _context.ReferralCodes
            .OrderBy(createdAt => createdAt.CreatedAt).ToListAsync(cancellationToken: token);
        return result;
    }

    public async Task<ReferralCode> GetReferralCodeByReferralCodeId(Guid referralCodeId, CancellationToken token = default)
    {
        var result = await _context.ReferralCodes
            .FirstOrDefaultAsync(referralCode => referralCode.Id == referralCodeId, cancellationToken: token);
        return result ?? throw new InvalidOperationException();
    }

    public async Task<bool> CreateReferralCode(ReferralCode referralCode, CancellationToken token = default)
    {
        var newReferralCode = new ReferralCode()
        {
            Id = Guid.NewGuid(),
            Code = GenerateReferralCode(),
            IsRedeemed = false,
            DiscountAmount = 50.00,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            UserId = referralCode.UserId
        };

        var referralCodeExist = await _context.ReferralCodes
            .AnyAsync(an => an.Code == referralCode.Code, cancellationToken: token);
        if (!referralCodeExist)
        {
            await _context.AddAsync(newReferralCode, token);
        }
        return await Save(token);
    }

    private string? GenerateReferralCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 14).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public async Task<bool> DeleteReferralCode(Guid referralCodeId, CancellationToken token = default)
    {
        var result = await _context.ReferralCodes
            .FirstOrDefaultAsync(id => id.Id == referralCodeId, cancellationToken: token);
        if (result == null)
        {
            return false; // Referral Code not found or already deleted
        }
        _context.Remove(result);
        return await Save(token);
    }
    
    public async Task<bool> ReferralCodeExistsId(Guid referralCodeId, CancellationToken token = default)
    {
        var resultExistsById =  await _context.ReferralCodes
            .AnyAsync(w => w.Id == referralCodeId, cancellationToken: token);
        return resultExistsById;
    }
    
    public async Task<bool> Save(CancellationToken token = default)
    {
        var saved =  await _context.SaveChangesAsync(token);
        return saved > 0;
    }
}