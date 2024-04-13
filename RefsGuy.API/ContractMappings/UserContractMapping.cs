using RefsGuy.Application.Models;
using RefsGuy.Contracts.Requests.UserRequests;
using RefsGuy.Contracts.Responses.UserResponses;

namespace RefsGuy.API.ContractMappings;

public static class UserContractMapping
{
    public static Users MapToUser(this CreateUserRequest? request)  //This maps the CreateUserDto to User
    {
        return new Users
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            City = request.City,
            Country = request.Country,
        };
    
    }
    
    public static Users MapToUser(this UpdateUserRequest request, Guid id) //This maps the UpdateCustomerDto to Customer
    {
        return new Users()
        {
            Id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            Email = request.Email,
            City = request.City,
            Country = request.Country,
        };
    }
    
    public static UserResponse MapsToResponse(this Users user) //This maps the User to UserResponse Dto
    {
        return new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            Email = user.Email,
            City = user.City,
            Country = user.Country,
            ReferralCode = user.ReferralCode
        };
    }
    
    
    public static UsersResponse MapsToResponse(this IEnumerable<Users> users) //This maps the list of users to the UsersResponses
    {
        return new UsersResponse()
        {
            User = users.Select(MapsToResponse)
        };
    }
}