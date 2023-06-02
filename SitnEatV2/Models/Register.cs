using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SitnEatV2.Models
{
    public class Register
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Unesite ime.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Unesite prezime.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Unesite email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unesite broj telefona.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Unesite password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potvrdite password.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password nije potvrđen!")]
        public string ConfirmPassword { get; set; }
    }
}
