using INF27507_Boutique_En_Ligne.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

/*
 * Tout le crédit des idées utilisées dans cette classe doit être
 * porté au site Binary Intellect. Repéré à http://www.binaryintellect.net/articles/b957238b-e2dd-4401-bfd7-f0b8d984786d.aspx
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
        private readonly SignInManager<IdentityUser> _loginManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(IDatabaseAdapter database, 
                              UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> loginManager,
                              RoleManager<IdentityRole> roleManager)
        {
            _database = database;
            _userManager = userManager;
            _loginManager = loginManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("/api/register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromForm] Register register)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityUser user = new IdentityUser();
                    user.UserName = register.Username;
                    user.Email = register.Email;

                    IdentityResult result = _userManager.CreateAsync(user, register.Password).Result;

                    if (result.Succeeded)
                    {
                        string roleName = register.Role.ToString();

                        if (!_roleManager.RoleExistsAsync(roleName).Result)
                        {
                            IdentityRole role = new IdentityRole();
                            role.Name = roleName;

                            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
                            if (!roleResult.Succeeded)
                            {
                                return BadRequest($"Error while creating role {roleName} !");
                            }
                        }

                        _userManager.AddToRoleAsync(user, roleName).Wait();
                        return Ok("The user was created successfully.");
                    }
                    return BadRequest(result.Errors.ToList());
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
        public IActionResult Login([FromForm] Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _loginManager.PasswordSignInAsync(login.Username, login.Password, false, false).Result;

                    if (result.Succeeded)
                    {
                        return Ok("Login successful");
                    }
                    return BadRequest(result);
                }
                return BadRequest("Login info invalid");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
