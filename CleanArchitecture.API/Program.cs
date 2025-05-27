// CleanArchitecture.API/Program.cs
using ComplexCalculator.Application;
using ComplexCalculator.Infrastructure.Identity;
using ComplexCalculator.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ComplexCalculator.API.Hubs;
using ComplexCalculator.API.HubExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// Add CORS services and configure policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient",
        policy => policy
            .WithOrigins(["http://localhost:4200", "http://121.37.227.251:8080", "http://47.239.123.32:8080", "http://47.239.123.32:8050"])  // Frontend origin to allow
            .AllowAnyMethod()                       // Allow all HTTP methods (GET, POST, etc.)
            .AllowAnyHeader()                       // Allow any headers (Authorization, Content-Type, etc.)
            .AllowCredentials());                   // If you want to send cookies/auth info
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowAngularDevClient");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Replace UseEndpoints with top-level route registrations
app.MapControllers();
app.MapHubEndpoints(); // Use the extension method here

// Call CreateRoles on app start
//var serviceProvider = app.Services.CreateScope().ServiceProvider;
//await CreateRoles(serviceProvider);
app.Run();

//async Task CreateRoles(IServiceProvider serviceProvider)
//{
//    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//    string[] roleNames = { "Admin", "User" };
//    foreach (var roleName in roleNames)
//    {
//        if (!await roleManager.RoleExistsAsync(roleName))
//        {
//            await roleManager.CreateAsync(new IdentityRole(roleName));
//        }
//    }
//}
