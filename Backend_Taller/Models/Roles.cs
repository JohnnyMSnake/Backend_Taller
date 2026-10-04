namespace Backend_Taller.Models
{
    public class Roles
    {
        public Guid RolesId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public ICollection<Users> User { get; set; } = new List<Users>();
    }
}
