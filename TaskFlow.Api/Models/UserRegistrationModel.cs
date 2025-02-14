using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.Models
{
    public class UserRegistrationModel
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
