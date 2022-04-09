using INF27507_Boutique_En_Ligne.Models;
using Microsoft.AspNetCore.Identity;

/*
 * Tout le crédit des idées utilisées dans cette classe doit être
 * porté au site Binary Intellect. Repéré à http://www.binaryintellect.net/articles/5e180dfa-4438-45d8-ac78-c7cc11735791.aspx
 */

namespace Api
{
    public static class AuthDbContextInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("DefaultUser").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "DefaultUser";
                user.Email = "default.user@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Client").Wait();
                }
            }

            if (userManager.FindByNameAsync("JacquesFerland").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "JacquesFerland";
                user.Email = "jacques.ferland@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Client").Wait();
                }
            }

            if (userManager.FindByNameAsync("NaNana").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "NaNana";
                user.Email = "na.nana@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Client").Wait();
                }
            }

            if (userManager.FindByNameAsync("NanaNana").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "NanaNana";
                user.Email = "nana.nana@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Client").Wait();
                }
            }

            if (userManager.FindByNameAsync("MilieuPage").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "MilieuPage";
                user.Email = "milieu.page@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Client").Wait();
                }
            }

            if (userManager.FindByNameAsync("TCan").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "TCan";
                user.Email = "t.can@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Client").Wait();
                }
            }

            if (userManager.FindByNameAsync("Jean-ChristopheTest").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "Jean-ChristopheTest";
                user.Email = "jean-christophe.test@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Client").Wait();
                }
            }

            if (userManager.FindByNameAsync("ClaudeLegault").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "ClaudeLegault";
                user.Email = "claude.legault@outlook.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Client").Wait();
                }
            }

            if (userManager.FindByNameAsync("AlbertJean").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "AlbertJean";
                user.Email = "albert.vendeur@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Seller").Wait();
                }
            }

            if (userManager.FindByNameAsync("AmelieMichel").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "AmelieMichel";
                user.Email = "amelie.vendeur@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Seller").Wait();
                }
            }

            if (userManager.FindByNameAsync("JuliePierre").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "JuliePierre";
                user.Email = "julie.vendeur@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Seller").Wait();
                }
            }

            if (userManager.FindByNameAsync("JeanMarchand").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "JeanMarchand";
                user.Email = "jean.marchand@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Seller").Wait();
                }
            }

            if (userManager.FindByNameAsync("XavierHenri").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "XavierHenri";
                user.Email = "xavier.vendeur@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Seller").Wait();
                }
            }

            if (userManager.FindByNameAsync("Jean-Christophe-Vendeur").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "Jean-Christophe-Vendeur";
                user.Email = "jean-christophe.test@gmail.com";

                IdentityResult result = userManager.CreateAsync(user, "MyPassword_123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Seller").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (UserType type in Enum.GetValues(typeof(UserType)))
            {
                string typeName = type.ToString();

                if (!roleManager.RoleExistsAsync(typeName).Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = typeName;

                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }
            }
        }
    }
}
