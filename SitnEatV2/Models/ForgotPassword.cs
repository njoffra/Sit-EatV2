using System.ComponentModel.DataAnnotations;

namespace SitnEatV2.Models
{
    public class ForgotPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
