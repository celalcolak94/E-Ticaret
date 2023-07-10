using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Kullanıcı adı giriniz.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre giriniz.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
