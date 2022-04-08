using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace INF27507_Boutique_En_Ligne.Models
{
    public abstract class User : IdentityUser<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
