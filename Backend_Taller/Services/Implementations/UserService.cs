using Backend_Taller.EFConfiguration;
using Backend_Taller.Models;
using Backend_Taller.Services.Interfaces;

namespace Backend_Taller.Services.Implementations
{
    public class UserService : IUserService
    {
        public readonly TallerDbContext _context;
        public UserService(TallerDbContext context)
        {
            _context = context;
        }
        public bool ValidateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
