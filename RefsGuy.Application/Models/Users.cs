namespace RefsGuy.Application.Models;

public class Users
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<ReferralCode> ReferralCode { get; set; } = new List<ReferralCode>();
}