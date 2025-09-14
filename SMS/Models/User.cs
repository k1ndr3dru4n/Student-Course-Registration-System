using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SMS.Models;

[Table("SMS_Users")]
public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string? Username { get; set; }

    [Required]
    [StringLength(100)]
    public string? Password { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? LastLogin { get; set; }
    public bool IsActive { get; set; } = true;
}

