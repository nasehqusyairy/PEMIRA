using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.Migrations;
using PEMIRA.Models;
using PEMIRA.ViewModels;
using System.Net;


namespace PEMIRA.Services
{
    public class UserService : TableService<User>
    {
        private readonly DatabaseContext _context;
        private readonly List<long> _selectedTags;

        public UserService(DatabaseContext context, int limit = 10, List<long>? selectedTags = null) : base(limit)
        {
            _context = context;
            _selectedTags = selectedTags ?? new List<long>();
        }

        public override List<User> GetEntries(string search, int page, string orderBy, bool isAsc)
        {
            search = search.Trim();
            if (!ModelHelper.IsPropertyExist<User>(orderBy))
            {
                orderBy = "Name";
            }

            page = page < 1 ? 1 : page;

            IQueryable<User> query = _context.Users;
            if (_selectedTags.Count == 0)
            {
                query = query.Where(user =>
                    user.DeletedAt == null &&
                    (user.Name.ToUpper().Contains(search.ToUpper()) || user.Code.Contains(search)));
            }
            else
            {
                query = query
                    .Include(user => user.TagUsers)
                    .Where(user =>
                        user.DeletedAt == null &&
                        (user.Name.ToUpper().Contains(search.ToUpper()) || user.Code.Contains(search)) &&
                        user.TagUsers.Any(tagUser => _selectedTags.Contains(tagUser.TagId)));
            }

            query = isAsc
                ? query.OrderBy(user => EF.Property<object>(user, orderBy))
                : query.OrderByDescending(user => EF.Property<object>(user, orderBy));

            query = query.Skip((page - 1) * LimitEntry).Take(LimitEntry);

            return query.ToList();
        }
        public List<User> GetUsers() => [.. _context.Users.Where(user => user.DeletedAt == null).OrderBy(user => user.Gender).ThenBy(user => user.Name)];
        public override int GetTotalEntry(string search) => _context.Users.Count(user => user.DeletedAt == null && (user.Name.Contains(search) || user.Code.Contains(search)));
        public List<TagUser> GetTagUsers() => [.. _context.TagUsers.Include(tag => tag.Tag)];
        public int GetPageCount(string search)
        {
            return (int)Math.Ceiling(_context.Users.Count(user => user.DeletedAt == null && (user.Name.Contains(search) || user.Code.Contains(search))) / (double)LimitEntry);
        }
        public User? IsUserUnique(string name, string code) => _context.Users.Where(user => user.Name == name || user.Code == code).FirstOrDefault(user => user.DeletedAt == null);
        public User? GetUserById(long userId) => _context.Users.First(id => id.Id == userId);
        public void Store(User user, long idUserNow)
        {
            user.CreatedBy = idUserNow;
            user.UpdatedBy = idUserNow;
            user.Name = user.Name.ToUpper();
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

        public List<Tag> GetTags() => [.. _context.Tags];
    }
}