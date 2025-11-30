using PEMIRA.Models;
using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.ViewModels;
namespace PEMIRA.Services
{
    public class TagUserService(DatabaseContext context, long id, int limit = 10) : TableService<TagUser>(limit)
    {
        private readonly DatabaseContext _context = context;
        public override List<TagUser> GetEntries(string search, int page, string orderBy, bool isAsc)
        {
            page = page < 1 ? 1 : page;
            search = search.Trim();
            IQueryable<TagUser> query = _context.TagUsers
                .Include(tag => tag.Tag)
                .Include(user => user.User)
                .Where(tag => tag.UserId == id && tag.Tag.Name.Contains(search));
            query = isAsc ? query.OrderBy(tag => tag.Tag.Name) : query.OrderByDescending(tag => tag.Tag.Name);
            query = query.Skip((page - 1) * LimitEntry).Take(LimitEntry);
            return [.. query];
        }
        public override int GetTotalEntry(string search) => _context.Tags.Count(tag => tag.DeletedAt == null && tag.Name.Contains(search));
        public List<Tag> GetTags() => [.. _context.Tags.Where(tag => tag.DeletedAt == null)];
        public TagUser? GetTagUserById(long tagId) => _context.TagUsers.FirstOrDefault(tag => tag.TagId == tagId && tag.UserId == id);
        public Tag? IsTagExist(string name)
        {
            return _context.Tags.FirstOrDefault(tag => tag.Name == name && tag.DeletedAt == null);
        }
        public bool IsTagUserExist(long id, long user) => _context.TagUsers.Any(tag => tag.TagId == id && tag.UserId == user);
        public void Store(Tag tag, long UserId, long id)
        {
            tag.CreatedBy = UserId;
            tag.CreatedAt = DateTime.Now;
            tag.UpdatedAt = DateTime.Now;
            _context.Tags.Add(tag);
            _context.SaveChanges();
            StoreTagUser(tag.Name, id);
        }
        public void StoreTagUser(string name, long id)
        {
            Tag? tag = _context.Tags.FirstOrDefault(tag => tag.Name == name);
            if (tag != null)
            {
                _context.TagUsers.Add(
                    new TagUser
                    {
                        UserId = id,
                        TagId = tag.Id
                    }
                );
            }
            _context.SaveChanges();
        }
        public void Delete(long id)
        {
            TagUser? tagUser = _context.TagUsers.FirstOrDefault(tag => tag.Id == id);
            if (tagUser != null)
            {
                _context.TagUsers.Remove(tagUser);
                _context.SaveChanges();
            }
        }
    }
}