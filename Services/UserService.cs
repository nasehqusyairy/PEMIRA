using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.Migrations;
using PEMIRA.Models;
using PEMIRA.ViewModels;
using System.Net;


namespace PEMIRA.Services
{
    public class UserService(DatabaseContext context, int limit = 10) : TableService<User>(limit)
    {
        private readonly DatabaseContext _context = context;
        public override List<User> GetEntries(string search, int page, string orderBy, bool isAsc)
        {
            if (!ModelHelper.IsPropertyExist<User>(orderBy))
            {
                orderBy = "Name";
            }
            page = page < 1 ? 1 : page;
            IQueryable<User> query = _context.Users.
                Where(User => User.DeletedAt == null && User.Name.Contains(search));
            query = isAsc ? query.OrderBy(user => EF.Property<object>(user, orderBy)) : query.OrderByDescending(user => EF.Property<object>(user, orderBy));
            query = query.Skip((page - 1) * LimitEntry).Take(LimitEntry);
            return [.. query];
        }
        public override int GetTotalEntry(string search) => _context.Users.Count(user => user.DeletedAt == null && user.Name.Contains(search));
        public List<TagUser> TagUserList() => _context.TagUsers.Include(tag => tag.Tag).ToList();
        public int GetPageCount(string search)
        {
            return (int)Math.Ceiling(_context.Users.Count(user => user.DeletedAt == null && user.Name.Contains(search)) / (double)LimitEntry);
        }
        public User? IsUserUnique(string name, long id) => _context.Users.FirstOrDefault(user => user.Name == name && user.DeletedAt == null && user.Id != id);
        public User? GetUserById(long userId) => _context.Users.First(id => id.Id == userId);
        public void Store(User user, long idUserNow)
        {
            user.CreatedBy = idUserNow;
            user.UpdatedBy = idUserNow;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void Update(UserViewModel input, User user, long idUserNow)
        {
            ModelHelper.UpdateProperties(input, user);
            user.CreatedBy = idUserNow;
            user.UpdatedBy = idUserNow;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void SoftDelete(User user, long idUserNow)
        {
            user.DeletedBy = idUserNow;
            user.DeletedAt = DateTime.Now;
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}