using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebStore.Models;

namespace WebStore.Services.AuthenticationService
{
    public class TokenService
    {
        private readonly UserManager<User> _userManager;

        public TokenService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        private const int ExpirationMinutes = 30;

        // Method to generate token and return detailed response
        public async Task<object> GenerateTokenResponse(User user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
            var roles = await _userManager.GetRolesAsync(user);

            var jwtToken = CreateJwtToken(
                CreateClaims(user, roles),
                CreateSigningCredentials(),
                expiration
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(jwtToken);

            return new
            {
                token = tokenString,
                roles = roles, // Explicitly include roles in the response
                userId = user.Id,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                expiresAt = expiration
            };
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration) =>
            new JwtSecurityToken(
                issuer: "MyIssuer",
                audience: "MyAudience",
                expires: expiration,
                claims: claims,
                signingCredentials: credentials
            );

        private List<Claim> CreateClaims(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Add user ID as a claim
                new Claim(JwtRegisteredClaimNames.Email, user.Email), // Add email claim
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // Add each role as a claim
            }

            return claims;
        }

        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes("Mmx44IfURe84Ac4i0g2eY8mDEhzUzXyyVPwKIo2SU")
                ),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
