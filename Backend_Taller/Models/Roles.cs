namespace Backend_Taller.Models
{
    public class Roles
    {
        public Guid RoleId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public ICollection<User> User { get; set; } = new List<User>();
    }
}
