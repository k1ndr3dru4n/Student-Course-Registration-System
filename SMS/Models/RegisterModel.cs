using System.ComponentModel.DataAnnotations;
using SMS.Models;

namespace SMS.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        [StringLength(50, ErrorMessage = "用户名长度必须在3-50个字符之间", MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "请输入邮箱")]
        [EmailAddress(ErrorMessage = "请输入有效的邮箱地址")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "请输入密码")]
        [StringLength(100, ErrorMessage = "密码长度必须在6-100个字符之间", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "请确认密码")]
        [Compare("Password", ErrorMessage = "两次输入的密码不一致")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "请选择用户角色")]
        public UserRole Role { get; set; } = UserRole.学生;
    }
}
