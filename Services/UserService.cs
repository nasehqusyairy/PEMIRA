using PEMIRA.Helpers;
using PEMIRA.Migrations;
using PEMIRA.Models;
using PEMIRA.ViewModels;

namespace PEMIRA.Services
{
    public class UserService(DatabaseContext context)
    {
        private readonly DatabaseContext _context = context;
        public List<User> GetUsers() => [.. _context.Users];
        public User? GetUserById(long userId) => _context.Users.First(id =>id.Id == userId);
        public User? GetUserByCode(string codeUser) => _context.Users.Where(code => code.Code == codeUser).FirstOrDefault();
        public void Store(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void Update(UserViewModel input)
        {
            User user = _context.Users.First(u => u.Id == input.Id);
            ModelHelper.UpdateProperties(input, user);
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}