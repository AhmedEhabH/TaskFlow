using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.Models
{
    public class UserRoleAssignmentModel
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = string.Empty;
    }

    enum ROLES {
        Admin,
        User
    }
}
