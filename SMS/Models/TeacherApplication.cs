using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace SMS.Models;

[Table("SMS_TeacherApplications")]

public class TeacherApplication
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(20)]
    public string CID { get; set; } = string.Empty; // 移除Required，由系统自动分配
    
    [Required]
    [Range(1, 10)]
    public int Credits { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [Required]
    public int TeacherId { get; set; }
    
    public Teacher? Teacher { get; set; }
    
    [Required]
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    
    public string? AdminComment { get; set; }
    
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}
    
public enum ApplicationStatus
{
    Pending = 0,    // 待审批
    Approved = 1,   // 已通过
    Rejected = 2    // 已拒绝
}
