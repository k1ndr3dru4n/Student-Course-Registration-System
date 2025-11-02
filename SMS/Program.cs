using Microsoft.EntityFrameworkCore;
using SMS;
using SMS.Components;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 添加Ant Design服务
builder.Services.AddAntDesign();

// 配置JSON序列化选项
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});

// 配置HTTPS 运行项目时应该注释掉
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7286; // 指定HTTPS端口
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
});

builder.Services.AddDbContextFactory<CimContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CimContext")));

builder.Services.AddQuickGridEntityFrameworkAdapter();

var app = builder.Build();

// 确保数据库是最新的
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CimContext>();
    context.Database.EnsureCreated(); // 这将创建数据库和所有表（如果它们不存在）
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
