using INF27507_Boutique_En_Ligne.Models;

namespace Api
{
    public class UserContext
    {
        public string? Id { get; set; }
        public string? Jti { get; set; }
        public string? UserName { get; set; }
        public UserType Role { get; set; }
    }
}
