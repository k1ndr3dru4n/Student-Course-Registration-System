using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string? CreateUser { get; set; }
    public string? UpdateUser { get; set; }

    public string? Username { get; set; } // 新增字段

    // 导航属性
    /// <summary>
    /// 该教师授课的所有课程
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}