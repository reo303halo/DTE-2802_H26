using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CupcakeMVC.Models.Entities;

public class Cupcake
{
    public int Id { get; set; }
    [MaxLength(32)]
    public string Name { get; set; } = "New Cupcake";
    [MaxLength(255)]
    public string? Description { get; set; }
    
    public int SizeId { get; set; }
    public Size? Size { get; set; }
    
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public string OwnerId { get; set; }
    public IdentityUser Owner { get; set; }
}