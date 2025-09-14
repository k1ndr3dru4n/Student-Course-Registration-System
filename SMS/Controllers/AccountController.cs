using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMS.Models;
using System.Security.Claims;

namespace SMS.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IDbContextFactory<CimContext> _contextFactory;

        public AccountController(IDbContextFactory<CimContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        [HttpGet("ProcessLogin")]
        public async Task<IActionResult> ProcessLogin(string username, bool rememberMe = false)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

                if (user != null)
                {
                    // 创建用户声明
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username!),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email!)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = rememberMe,
                        ExpiresUtc = rememberMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddHours(1)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // 跳转到目标页面
                    return Redirect("http://localhost:5286/home");
                }
                else
                {
                    return Redirect("/login?error=用户不存在");
                }
            }
            catch (Exception ex)
            {
                return Redirect($"/login?error={Uri.EscapeDataString($"认证过程中发生错误: {ex.Message}")}");
            }
        }
    }
}
