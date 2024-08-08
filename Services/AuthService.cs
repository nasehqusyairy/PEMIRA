using Microsoft.EntityFrameworkCore;
using PEMIRA.Models;
namespace PEMIRA.Services
{
    public class AuthService(DatabaseContext context)
    {
        private readonly DatabaseContext _context = context;

        public User? GetUserByCode(string code, long electionId) => _context.Users
        .Include(user => user.RoleUsers.Where(roleUser => roleUser.ElectionId == electionId))
        .ThenInclude(roleUser => roleUser.Role)
        .FirstOrDefault(user => user.Code == code);
    }
}
