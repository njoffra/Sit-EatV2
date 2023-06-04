using System.ComponentModel.DataAnnotations;

namespace SitnEatV2.Models
{
    public class Login
    {


        [Required(ErrorMessage = "Niste unijeli važeći email!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Netačna šifra!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
