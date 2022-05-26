using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using webChat.Data;
using webChat.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<webChatContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("webChatContext") ?? throw new InvalidOperationException("Connection string 'webChatContext' not found.")));

builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    //options.Cookie.HttpOnly = true;
    //options.Cookie.IsEssential = true;
});
//builder.Services.AddMvc();



//builder.Services.AddDistributedMemoryCache();

//builder.Services.AddRazorPages();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login/";
    options.AccessDeniedPath = "/Accuont/AccessDenied/";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

//app.MapRazorPages();

//app.UseStatusCodePages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();