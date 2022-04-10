using INF27507_Boutique_En_Ligne.Models;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api
{
    public class JwtHandler : IJwtHandler
    {
        private readonly IConfiguration configuration;

        public JwtHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public JwtSecurityToken GetToken(IdentityUser user, IList<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public UserContext GetUserContext(HttpRequest request)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            string authorizationHeader = request.Headers["Authorization"];
            authorizationHeader = authorizationHeader.Replace("Bearer ", "");
            JwtSecurityToken token = jwtHandler.ReadJwtToken(authorizationHeader);

            string jti = token.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;
            string userName = token.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            UserType role = Enum.Parse<UserType>(token.Claims.First(claim => claim.Type == ClaimTypes.Role).Value);

            return new UserContext()
            {
                Jti = jti,
                UserName = userName,
                Role = role
            };
        }

        public string WriteToken(JwtSecurityToken token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
