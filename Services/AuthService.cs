using Microsoft.EntityFrameworkCore;
using PEMIRA.Models;
namespace PEMIRA.Services
{
    public class AuthService
    {
        public AuthService(DatabaseContext context) => _context = context;

        public AuthService()
        {
            _context = new DatabaseContext();
        }

        private readonly DatabaseContext _context;

        public User? GetUserByCode(string code, long electionId) => _context.Users
        .Include(user => user.RoleUsers.Where(roleUser => roleUser.ElectionId == electionId))
        .ThenInclude(roleUser => roleUser.Role)
        .FirstOrDefault(user => user.Code == code);

        public List<Election> GetElections() => [.. _context.Elections];
    }
}
