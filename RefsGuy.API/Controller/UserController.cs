using Microsoft.AspNetCore.Mvc;
using RefsGuy.API.ContractMappings;
using RefsGuy.Application.Interfaces;
using RefsGuy.Contracts.Requests.UserRequests;
using RefsGuy.Contracts.Responses;
using RefsGuy.Contracts.Responses.UserResponses;

namespace RefsGuy.API.Controller;

public class UserController(IUserRepository userRepository) : ControllerBase
{
    //GET all Users
    [HttpGet(ApiEndpoints.User.GetAll)]
    public async Task<IActionResult> GetUsers(CancellationToken token)
    {
        var user = await userRepository.GetUsersAsync(token);
        var userResponse = new FinalResponse<UsersResponse>
        {
            StatusCode = 200,
            Message = "Users retrieved successfully.",
            Data = user.MapsToResponse()
        };
        return Ok(userResponse);
    }
    
    //GET UserById
    [HttpGet(ApiEndpoints.User.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
    {
        var users = await userRepository.GetUsersById(id, token);
        if (users == null)
        {
            return NotFound(new FinalResponse<object>
            {
                StatusCode = 404,
                Message = "User not found."
            });
        }
        
        var userResponse = new FinalResponse<UserResponse>
        {
            StatusCode = 200,
            Message = "User retrieved successfully.",
            Data = users.MapsToResponse()
        };
        return Ok(userResponse);
    }
    
    
    //GET UserById
    // [HttpGet(ApiEndpoints.User.GetWallet)]
    // public async Task<IActionResult> GetWalletsByUserId([FromRoute] Guid id)
    // {
    //     var user = await _userRepository.GetWalletByUserId(id);
    //     if (user == null)
    //     {
    //         return NotFound(new FinalResponse<object>
    //         {
    //             StatusCode = 404,
    //             Message = "User not found."
    //         });
    //     }
    //     
    //     var userResponse = new FinalResponse<object>
    //     {
    //         StatusCode = 200,
    //         Message = "User retrieved successfully.",
    //         Data = user.MapsToResponse()
    //     };
    //     return Ok(userResponse);
    // }
    
    //POST User
    
    [HttpPost(ApiEndpoints.User.Create)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest? request, CancellationToken token)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "User data is invalid." });
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }
        
        var mapToUser = request.MapToUser();
        await userRepository.CreateUser(mapToUser ?? throw new InvalidOperationException(), token);
        var userResponse = new FinalResponse<UserResponse>
        {
            StatusCode = 201,
            Message = "User created successfully.",
            Data = mapToUser.MapsToResponse()
        };
        return CreatedAtAction(nameof(Get), new { id = mapToUser.Id }, userResponse);
    }
    
    //UPDATE User
    [HttpPut(ApiEndpoints.User.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserRequest request, CancellationToken token)
    {
        if (request is null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "User data is invalid." });
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }
        var mapToUser = request.MapToUser(id);
        var updatedUser = await userRepository.UpdateUser(mapToUser, token);
        if (updatedUser is false)
        {
            return NotFound(new FinalResponse<object>
            {
                StatusCode = 404,
                Message = "User not found."
            });
        }
        var response = new FinalResponse<UserResponse>
        {
            StatusCode = 200,
            Message = "User details updated successfully.",
            Data = mapToUser.MapsToResponse()
        };
        return Ok(response);

    }

    //DELETE User 
    [HttpDelete(ApiEndpoints.User.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await userRepository.UsersExists(id, token);
        var user = await userRepository.DeleteUser(id, token);
        if (!user)
        {
            return NotFound(new FinalResponse<string>
            {
                StatusCode = 404,
                Message = "User not found or already deleted",
                Data = null
            });
        }
        
        return Ok(new FinalResponse<string>
            {
                StatusCode = 200,
                Message = "User deleted successfully",
                Data = null
            });
    }
}