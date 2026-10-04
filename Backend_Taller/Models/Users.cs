using System.ComponentModel.DataAnnotations;

namespace Backend_Taller.Models
{
    public class Users
    {
        public int UsersId { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Guid RolesId { get; set; }
        public Roles Role { get; set; }

    }
}
