using Microsoft.AspNetCore.Identity;

namespace ComputerApi.Models;

public class CustomUser : IdentityUser
{
    public string LastName { get; set; } = "";
    public string FirstName { get; set; } = "";
}