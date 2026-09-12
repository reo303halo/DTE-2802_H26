using Microsoft.AspNetCore.Identity;

namespace ComputerApi.Models.Entities;

public class CustomUser : IdentityUser
{
    public string LastName { get; set; } = "";
    public string FirstName { get; set; } = "";
    
    // Not neccessary - but we want to not only be able to use
    // computer.Owner
    // but ALSO
    // owner.Computers
    public ICollection<Computer> Computers { get; set; } = [];
}