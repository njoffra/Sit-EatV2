using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SitnEatV2.Models
{
	public class Register
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue(0)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        //[Required]

        public string? LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage ="Password nije potvrđen!")]
		public string ConfirmPassword { get; set; }
    }
}
