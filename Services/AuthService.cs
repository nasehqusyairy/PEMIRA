using Microsoft.EntityFrameworkCore;
using PEMIRA.Models;
namespace PEMIRA.Services
{
    public class AuthService(DatabaseContext context)
    {
        private readonly DatabaseContext _context = context;

        public User? GetUserByCode(string code) => _context.Users.FirstOrDefault(user => user.Code == code);

        public RoleUser? GetRoleUserByUserId(long userId, long electionId) => _context.RoleUsers.FirstOrDefault(roleUser => roleUser.UserId == userId && roleUser.ElectionId == electionId);
    }
}
