using Microsoft.EntityFrameworkCore;
using RefsGuy.Application.Database;
using RefsGuy.Application.Interfaces;
using RefsGuy.Application.Models;

namespace RefsGuy.Application.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Users>> GetUsersAsync(CancellationToken token = default)
    {
        var result = await _context.Users
            .Include(referralCode => referralCode.ReferralCode)
            .ToListAsync(cancellationToken: token);
        return result;
    }

    public async Task<Users> GetUsersById(Guid id, CancellationToken token = default)
    {
        var result = await _context.Users
            .Include(referralCode => referralCode.ReferralCode)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken: token);
        return result ?? throw new InvalidOperationException();
    }

    public async Task<bool> CreateUser(Users user, CancellationToken token = default)
    {
        var newUser = new Users()
        {
            Id = Guid.NewGuid(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Address = user.Address,
            Email = user.Email,
            City = user.City,
            Country = user.Country,
            PhoneNumber = user.PhoneNumber
        };
        await _context.AddAsync(newUser, token);
        return await Save(token);
    }

    public async Task<bool> UpdateUser(Users user, CancellationToken token = default)
    {
        var result = await _context.Users.FirstOrDefaultAsync(p => 
            p.Id  == user.Id, cancellationToken: token);
        
        if (result != null)
        {
            result.FirstName = user.FirstName;
            result.LastName = user.LastName;
            result.Address = user.Address;
            result.Email = user.Email;
            result.Country = user.Country;
            result.PhoneNumber = user.PhoneNumber;
            result.City = user.City;
        }
        return await Save(token);
    }

    public async Task<bool> DeleteUser(Guid id, CancellationToken token = default)
    {
        var result = await _context.Users.FirstOrDefaultAsync(i => 
            i.Id == id, cancellationToken: token);
        
        if (result == null)
        {
            return false; // Customer not found or already deleted
        }
        _context.Remove(result);
        return await Save(token);
    }

    public async Task<bool> UsersExists(Guid id, CancellationToken token = default)
    {
        var result =  await _context.Users.AnyAsync(c => c.Id == id, cancellationToken: token);
        return result;
    }

    public async Task<bool> Save(CancellationToken token = default)
    {
        var saved =  await _context.SaveChangesAsync(token);
        return saved > 0;
    }
}