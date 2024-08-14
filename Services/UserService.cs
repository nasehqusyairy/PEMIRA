using PEMIRA.Models;

namespace PEMIRA.Services
{
    public class UserService(DatabaseContext context)
    {
        private readonly DatabaseContext _context = context;
        public List<User> GetUsers() => [.. _context.Users];
        public User? GetUserByCode(string userCode) => _context.Users.Where(code => code.Code == userCode).FirstOrDefault();
        public void Store(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}