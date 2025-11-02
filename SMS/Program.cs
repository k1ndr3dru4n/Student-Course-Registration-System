using Microsoft.EntityFrameworkCore;
using SMS;
using SMS.Components;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 添加本地化服务 - 修复资源路径配置
builder.Services.AddLocalization();

// 添加HTTP上下文访问器
builder.Services.AddHttpContextAccessor();

// 配置支持的语言文化
var supportedCultures = new[]
{
    new CultureInfo("zh-CN"), // 中文
    new CultureInfo("en-US"), // 英文
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("zh-CN");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    // 设置请求文化提供程序的优先级
    options.RequestCultureProviders.Clear();
    
    // QueryString provider - 检查 ?culture=xx 参数
    var queryStringProvider = new QueryStringRequestCultureProvider
    {
        QueryStringKey = "culture",
        UIQueryStringKey = "culture"
    };
    options.RequestCultureProviders.Add(queryStringProvider);
    
    // Cookie provider
    options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
    
    // Browser language header provider
    options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
});

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

// 添加本地化中间件 - 对于Blazor Server，需要确保在路由之前
app.UseRequestLocalization(app =>
{
    var supportedCultures = new[] { "zh-CN", "en-US" };
    app.SetDefaultCulture("zh-CN")
       .AddSupportedCultures(supportedCultures)
       .AddSupportedUICultures(supportedCultures);
    
    // 配置查询字符串提供程序
    app.RequestCultureProviders.Clear();
    app.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());
    app.RequestCultureProviders.Add(new CookieRequestCultureProvider());
    app.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
});

app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
