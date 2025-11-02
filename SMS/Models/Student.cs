using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Models;
[Table("SMS_Students")]
public class Student
{
    public int Id { get; set; }

    public int SID { get; set; }

    public string? Name { get; set; }

    public int Age { get; set; }

    public string? Major { get; set; }
    
    public string? Email { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string? CreateUser { get; set; }
    public string? UpdateUser { get; set; }
}