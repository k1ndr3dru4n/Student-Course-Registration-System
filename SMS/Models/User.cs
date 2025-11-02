using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SMS.Models;

public enum UserRole
{
    学生 = 0,
    老师 = 1,
    管理员 = 2
}

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

    /// <summary>
    /// 用户角色：学生、老师、管理员
    /// </summary>
    public UserRole Role { get; set; } = UserRole.学生;

    /// <summary>
    /// 关联的学生ID（当角色为学生时）
    /// </summary>
    public int? StudentId { get; set; }

    /// <summary>
    /// 关联的教师ID（当角色为老师时）
    /// </summary>
    public int? TeacherId { get; set; }

    // 已删除的数据库字段
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string? CreateUser { get; set; }
    public string? UpdateUser { get; set; }

    // 导航属性
    [ForeignKey("StudentId")]
    public virtual Student? Student { get; set; }

    [ForeignKey("TeacherId")]
    public virtual Teacher? Teacher { get; set; }
}

