using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Api
{
    public interface IJwtHandler
    {
        JwtSecurityToken GetToken(IdentityUser user, IList<string> roles);
        string WriteToken(JwtSecurityToken token);
        UserContext GetUserContext(HttpRequest request);
    }
}
