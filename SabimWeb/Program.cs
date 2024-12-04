using Microsoft.AspNetCore.Authorization;
using Sabim.Web.Extensions;
using Sabim.Web.Filters;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
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
builder.Services.AddSingleton<IAuthorizationHandler, ClaimBasedAuthorizationHandler>();
builder.Services.ConfigureAuthorization();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseMvcWithAreas();
app.Run();

