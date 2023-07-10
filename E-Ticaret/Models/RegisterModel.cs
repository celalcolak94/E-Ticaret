using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı alanı boş geçilemez.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ad alanı boş geçilemez.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad alanı boş geçilemez.")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "E-posta alanı boş geçilemez.")]
        [EmailAddress(ErrorMessage = "E-posta formatında giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola alanı boş geçilemez.")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[.#?!@$%^&*-]).{8,12}$",ErrorMessage = "Güvenli parola seçiniz, 8-12 karakter")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Parola alanı boş geçilemez.")]
        [Compare("Password",ErrorMessage = "Parolalar eşleşmiyor.")]
        public string RePassword { get; set; }
    }
}
