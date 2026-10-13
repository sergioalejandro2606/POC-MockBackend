using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TransferenciasMockApi.Models;
namespace TransferenciasMockApi.Controllers;

[ApiController, Route("api/auth")]
public sealed class AuthController(IConfiguration config) : ControllerBase {
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request) {
        if (request.Username != config["DemoCredentials:Username"] || request.Password != config["DemoCredentials:Password"])
            return Unauthorized(new ErrorResponse("AUTH_002", "Unauthorized", "Usuario o contraseña incorrectos."));
        var section = config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section["SigningKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(int.TryParse(section["ExpirationMinutes"], out var m) ? m : 60);
        var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, request.Username), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), new Claim(ClaimTypes.Name, request.Username) };
        var token = new JwtSecurityToken(section["Issuer"], section["Audience"], claims, DateTime.UtcNow, expires, creds);
        return Ok(new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token), "Bearer", (int)(expires-DateTime.UtcNow).TotalSeconds));
    }
}
