using System.ComponentModel.DataAnnotations;

namespace SitnEatV2.Models
{
    public class Login
    {


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Netačan email ili šifra!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
