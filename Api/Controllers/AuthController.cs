using INF27507_Boutique_En_Ligne.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

/*
 * Tout le crédit des idées utilisées dans cette classe doit être
 * porté à M. Sarathlal Saseendran. 
 * Repéré à https://www.c-sharpcorner.com/article/jwt-authentication-and-authorization-in-net-6-0-with-identity-framework/
 */

namespace Api.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IDatabaseAdapter _database;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtHandler _jwtHandler;

        public AuthController(IDatabaseAdapter database, 
                              UserManager<IdentityUser> userManager,
                              IJwtHandler jwtHandler)
        {
            _database = database;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        [HttpPost]
        [Route("/api/register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromForm] Register register)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityUser userExists = await _userManager.FindByNameAsync(register.Username);
                    if (userExists != null)
                        return StatusCode(StatusCodes.Status500InternalServerError, "User already exists!");

                    IdentityUser user = new IdentityUser()
                    {
                        Email = register.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = register.Username
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, register.Password);
                    if (!result.Succeeded)
                        return StatusCode(StatusCodes.Status500InternalServerError, "User creation failed! Please check user details and try again.");

                    _userManager.AddToRoleAsync(user, register.Role.ToString()).Wait();
                    return Ok("The user was created successfully.");
                }
                return BadRequest("Invalid register information");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error has occured: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("/api/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromForm] Login login)
        {
            try
            {
                IdentityUser user = await _userManager.FindByNameAsync(login.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
                {
                    IList<string> roles = await _userManager.GetRolesAsync(user);
                    JwtSecurityToken token = _jwtHandler.GetToken(user, roles);

                    return Ok(_jwtHandler.WriteToken(token));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error has occured: {ex.Message}");
            }
        }
    }
}
