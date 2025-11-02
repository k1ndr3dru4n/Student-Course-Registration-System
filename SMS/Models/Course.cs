using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SMS.Models;

[Table("SMS_Courses")]
public class Course
{
    public int Id { get; set; }
    public int CID { get; set; }
    public string? Name { get; set; }
    public int Credits { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; } = true;
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string? CreateUser { get; set; }
    public string? UpdateUser { get; set; }
}

