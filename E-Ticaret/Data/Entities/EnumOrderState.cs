using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Data.Entities
{
    public enum EnumOrderState
    {
        [Display(Name = "Onay Bekleniyor.")]
        Waiting,

        [Display(Name = "Tamamlandı.")]
        Complated
    }
}