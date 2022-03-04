using Microsoft.AspNetCore.Identity;

namespace lk_api.UsersDatabase
{
    public class User : IdentityUser
    {
        public int AbonentId { get; set; }
    }
}
