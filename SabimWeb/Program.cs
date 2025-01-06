using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using NLog;
using Sabim.Web.Extensions;
using Sabim.Web.Filters;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.AddControllersWithViews();
builder.Services.ConfigureAddValidators();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureRegisterRepositories();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceRegistration();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureAutoMapper();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.ConfigureSession();
builder.Services.ConfigureApplicationCookie();
builder.Services.AddScoped<IAuthorizationHandler, ClaimBasedAuthorizationHandler>();
builder.Services.ConfigureAuthorization();
builder.Services.ConfigureMailSettings(builder.Configuration);
var app = builder.Build();
// Configure the HTTP request pipeline.
// Genel hata sayfasý
app.UseExceptionHandler("/Error/{0}");
// 404 ve diðer hata sayfalarýný yönlendirme
app.UseStatusCodePagesWithReExecute("/Error/{0}");
// HTTP Strict Transport Security (HSTS) ayarlarý
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseMvcWithAreas();
app.Run();

