using System.ComponentModel.DataAnnotations;

namespace lk_api.UsersDatabase.Models
{
    public class LoginModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
