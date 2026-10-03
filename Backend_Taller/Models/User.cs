using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.Models
{
    public class User
    {
        public int UserId { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public Roles Role { get; set; }

    }
}
