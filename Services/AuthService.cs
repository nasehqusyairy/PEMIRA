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

        public User? GetUserByCode(string code) => _context.Users
        .Include(user => user.RoleUsers)
        .ThenInclude(roleUser => roleUser.Role)
        .FirstOrDefault(user => user.Code.Contains(code));
        public ElectionUser? GetParticipant(long userId, long electionId) => _context.ElectionUsers.FirstOrDefault(eu => eu.UserId == userId && eu.ElectionId == electionId);

        public List<Election> GetElections() => [.. _context.Elections];
    }
}
