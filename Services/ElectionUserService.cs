using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.Models;
namespace PEMIRA.Services
{
    public class ElectionUserService : TableService<ElectionUser>
    {
        private readonly DatabaseContext _context;
        private readonly List<long> _selectedTags;

        public ElectionUserService(DatabaseContext context, int limit = 10, List<long> selectedTags = null) : base(limit)
        {
            _context = context;
            _selectedTags = selectedTags ?? new List<long>();
        }
        public override List<ElectionUser> GetEntries(string search, int page, string orderBy, bool isAsc)
        {
            if (!ModelHelper.IsPropertyExist<ElectionUser>(orderBy))
            {
                orderBy = "User.Name"; 
            }
            page = page < 1 ? 1 : page;
            IQueryable<ElectionUser> query = _context.ElectionUsers
                .Where(electionUser => electionUser.User.Name.Contains(search));  

            if (_selectedTags != null && _selectedTags.Count > 0)
            {
                query = from electionUser in query
                        join tagUser in _context.TagUsers on electionUser.UserId equals tagUser.UserId
                        where _selectedTags.Contains(tagUser.TagId)
                        select electionUser;
            }
            query = isAsc
                ? query.OrderBy(electionUser => EF.Property<object>(electionUser, orderBy))
                : query.OrderByDescending(electionUser => EF.Property<object>(electionUser, orderBy));
            query = query.Skip((page - 1) * LimitEntry).Take(LimitEntry);

            return query.ToList();
        }

        public override int GetTotalEntry(string search)
        {
            return _context.ElectionUsers.Count(electionUser => electionUser.User.Name.Contains(search));
        }
        public List<TagUser> GetTagUsers() => _context.TagUsers.Include(tag => tag.Tag).ToList();
        public int GetPageCount(string search)
        {
            return (int)Math.Ceiling(_context.ElectionUsers.Count(electionUser => electionUser.User.Name.Contains(search)) / (double)LimitEntry);
        }
        public List<Tag> GetTags() => [.. _context.Tags];
    }
}
