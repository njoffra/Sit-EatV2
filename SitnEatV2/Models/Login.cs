using System.ComponentModel.DataAnnotations;

namespace SitnEatV2.Models
{
    public class Login
    {


        [Required(ErrorMessage = "Korisnik ne postoji!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Unesite ispravnu lozinku!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
