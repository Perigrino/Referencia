using RefsGuy.Application.Models;

namespace RefsGuy.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<Users>> GetUsersAsync(CancellationToken token = default);
    Task<Users> GetUsersById(Guid id, CancellationToken token = default);
    //Task<CustomerWallet> GetWalletByCustomerId(Guid id);
    Task<bool> CreateUser(Users user, CancellationToken token = default);
    Task<bool> UpdateUser(Users user, CancellationToken token = default);
    Task<bool> DeleteUser(Guid id, CancellationToken token = default);
    Task<bool> UsersExists(Guid id, CancellationToken token = default);
    Task<bool> Save(CancellationToken token = default);
}