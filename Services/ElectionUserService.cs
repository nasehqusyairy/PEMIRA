using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.Models;
namespace PEMIRA.Services
{
    public class ElectionUserService(DatabaseContext context, long electionId, int limit = 10, List<long>? selectedTags = null) : TableService<ElectionUser>(limit)
    {
        private readonly DatabaseContext _context = context;
        private readonly List<long> _selectedTags = selectedTags ?? [];
        private long ElectionId = electionId;

        public override List<ElectionUser> GetEntries(string search, int page, string orderBy, bool isAsc)
        {
            if (!ModelHelper.IsPropertyExist<User>(orderBy))
            {
                orderBy = "Name";
            }

            page = page < 1 ? 1 : page;

            IQueryable<ElectionUser> query = _context.ElectionUsers
                .Include(eu => eu.User)
                .Include(eu => eu.Election)
                .Where(eu => eu.User.Name.Contains(search) && eu.ElectionId == ElectionId);

            if (_selectedTags != null && _selectedTags.Count > 0)
            {
                query = query
                    .Include(eu => eu.User.TagUsers)
                    // .Where(eu => _selectedTags.Contains(eu.User.TagUsers.Select(tu => tu.TagId).FirstOrDefault()));
                    .Where(eu => eu.User.TagUsers.Any(tu => _selectedTags.Contains(tu.TagId)));
            }

            if (orderBy == "Name")
            {
                query = isAsc
                    ? query.OrderBy(eu => eu.User.Name)
                    : query.OrderByDescending(eu => eu.User.Name);
            }
            else
            {
                query = isAsc
                    ? query.OrderBy(eu => EF.Property<object>(eu.User, orderBy))
                    : query.OrderByDescending(eu => EF.Property<object>(eu.User, orderBy));
            }

            query = query.Skip((page - 1) * LimitEntry).Take(LimitEntry);

            return [.. query];
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
        public void AddParticipants(long[] selectedIds)
        {
            var enrolledUserIds = _context.ElectionUsers
                                          .Where(eu => eu.ElectionId == ElectionId)
                                          .Select(eu => eu.UserId)
                                          .ToHashSet();

            List<ElectionUser> electionUsers = new List<ElectionUser>();

            foreach (long id in selectedIds)
            {
                if (!enrolledUserIds.Contains(id))
                {
                    electionUsers.Add(new ElectionUser
                    {
                        UserId = id,
                        ElectionId = ElectionId
                    });
                }
            }

            if (electionUsers.Count > 0)
            {
                _context.ElectionUsers.AddRange(electionUsers);
                _context.SaveChanges();
            }
        }

        public void RemoveParticipant(long id)
        {
            ElectionUser? electionUser = _context.ElectionUsers.Find(id);
            if (electionUser != null)
            {
                _context.ElectionUsers.Remove(electionUser);
                _context.SaveChanges();
            }
        }
    }
}
