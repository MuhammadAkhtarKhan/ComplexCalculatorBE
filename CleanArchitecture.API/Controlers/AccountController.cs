// CleanArchitecture.API/Controllers/AccountController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ComplexCalculator.Infrastructure.Identity;
using System.Threading.Tasks;
using CleanArchitecture.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ComplexCalculator.Infrastructure;
using System.Linq;
using ComplexCalculator.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using ComplexCalculator.API.Hubs;
using ComplexCalculator.Application.Contracts.Calculator;
using System.Diagnostics;
using ComplexCalculator.Infrastructure.JWT;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ICalculator _calculator;
    private readonly IJwtTokenService _jwtTokenService;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext context,
        RoleManager<IdentityRole> roleManager,
        IJwtTokenService jwtTokenService,
        ICalculator calculator
        )
    {
        this._userManager = userManager;
        _signInManager = signInManager;
        this._context = context;
        this._roleManager = roleManager;
        _calculator = calculator;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            if (await _roleManager.RoleExistsAsync(model.Role))
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            return Ok(new { Result = "User registered successfully" });
        }
        return BadRequest(result.Errors);
    }

   
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var roles = await _userManager.GetRolesAsync(user);
            var batchNo = await _calculator.GetLatestBatchNo(user.Id);
            batchNo = batchNo == 0 ? 1 : batchNo + 1;

            var token = _jwtTokenService.GenerateToken(user, roles);

            return Ok(new
            {
                Token = token,
                User = new
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    Roles = roles,
                    BatchNo = batchNo
                }
            });
        }

        return Unauthorized();
    }



    [HttpGet("logout")]
    public async Task<IActionResult> Logout(string userEmail)
    {
        
        var user = await _userManager.FindByEmailAsync(userEmail);

        // Find the latest session with no LogoutTime (i.e., the active session)
        var session = await _context.UserSessions
            .Where(s => s.UserId == user.Id && s.LogoutTime == null)
            .OrderByDescending(s => s.LoginTime)
            .FirstOrDefaultAsync();

        if (session != null)
        {
            // Update the session with the logout time
            session.LogoutTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        await _signInManager.SignOutAsync();
        return Ok(new { Result = $"User {userEmail} has been logged out successfully." });
    }
    [HttpGet("loggedinusers")]
    public async Task<IActionResult> GetLoggedInUsersCount()
    {
        var loggedInUsersCount = await _context.UserSessions
       .CountAsync(s => s.LogoutTime == null);
        return Ok(new { LoggedInUsers = loggedInUsersCount });
    }
  
}

