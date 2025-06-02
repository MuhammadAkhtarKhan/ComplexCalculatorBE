// CleanArchitecture.API/Program.cs
using ComplexCalculator.Application;
using ComplexCalculator.Infrastructure.Identity;
using ComplexCalculator.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ComplexCalculator.API.Hubs;
using ComplexCalculator.API.HubExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add application and infrastructure services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add SignalR
builder.Services.AddSignalR();

// Configure EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add CORS services and configure policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient",
        policy => policy
            .WithOrigins(
                [
                    "http://localhost:4200",
                    "http://121.37.227.251:8080",
                    "http://121.37.227.251:8050",
                    "http://47.239.123.32:8080",
                    "http://47.239.123.32:8050"
                ])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Add JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

// Add Authorization
builder.Services.AddAuthorization();

// Add Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS
app.UseCors("AllowClient");

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger(); // Optional in production
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Authentication & Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapControllers();
app.MapHubEndpoints(); // SignalR hubs

app.Run();
