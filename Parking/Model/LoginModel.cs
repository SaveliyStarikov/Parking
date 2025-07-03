using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
namespace Parking.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        [EmailAddress(ErrorMessage = "Некорректный Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public ClaimsIdentity? Username { get; internal set; }
        public bool RememberMe { get; internal set; }
    }
}
