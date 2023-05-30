using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SitnEatV2.Models
{
    public class User
    {
        [DefaultValue(0)]

        public int Id { get; set; }

        [Required(ErrorMessage = "Unesite validnu email adresu!")]
        [EmailAddress]
        [Display(Name ="Email adresa")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Unesite šifru")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
