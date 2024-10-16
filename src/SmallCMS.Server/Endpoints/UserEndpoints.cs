using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmallCMS.API.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmallCMS.API.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapPost("/api/auth/login", async ([FromBody] LoginRequestDto loginRequest, UserManager<IdentityUser> userManager, IConfiguration config, ILogger<Program> logger) =>
            {
                try
                {
                    var user = await userManager.FindByEmailAsync(loginRequest.Email);
                    if (user == null)
                    {
                        return Results.Unauthorized();
                    }

                    var passwordValid = await userManager.CheckPasswordAsync(user, loginRequest.Password);
                    if (!passwordValid)
                    {
                        logger.LogWarning("Invalid password for user with email {Email}", loginRequest.Email);

                        return Results.Unauthorized();
                    }

                    var claims = new[]
                    {
                          new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim(ClaimTypes.NameIdentifier, user.Id),
                          new Claim(ClaimTypes.Email, user.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                     issuer: config["Jwt:Issuer"],
                     audience: config["Jwt:Audience"],
                     claims: claims,
                     expires: DateTime.UtcNow.AddDays(30),
                     signingCredentials: creds
                 );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Results.Ok(new { token = tokenString, email = user.Email });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred during login for user with email: {Email}", loginRequest.Email);
                    return Results.Problem("An error occurred while processing your request");
                }
            })
            .WithName("Login");

            return app;
        }
    }
}
