namespace RefsGuy.Contracts.Responses.UserResponses;

public class UsersResponse
{
    public required IEnumerable<UserResponse> User { get; init; } = Enumerable.Empty<UserResponse>();
}