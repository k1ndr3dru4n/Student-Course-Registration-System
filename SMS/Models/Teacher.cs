using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Models;

[Table("SMS_Teachers")]
public class Teacher
{
    public int Id { get; set; }

    public int TID { get; set; }

    public string? Name { get; set; }

    public int Age { get; set; }

    public string? Major { get; set; }
    
    public string? Email { get; set; }
}