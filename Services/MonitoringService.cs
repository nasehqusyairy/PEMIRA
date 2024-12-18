using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.Models;

namespace PEMIRA.Services
{
    public class MonitoringService(DatabaseContext context, int limit, long electionId, List<long>? selectedTags = null) : TableService<User>(limit)
    {
        private readonly DatabaseContext _context = context;
        private readonly List<long> _selectedTags = selectedTags ?? new List<long>();
        private readonly long electionId = electionId;
        private readonly long? _electionId = electionId;
        public List<Election> GetElections()
        {
            return [.. _context.Elections.Where(e => e.DeletedAt == null)];
        }

        public Election? GetElection()
        {
            return _context.Elections
              .Include(e => e.Candidates.Where(c => c.DeletedAt == null).OrderByDescending(c => c.CandidateUsers.Count))
                .ThenInclude(c => c.CandidateUsers)
              .Include(e => e.Candidates)
                .ThenInclude(c => c.User)
              .Include(e => e.ElectionUsers)
                .ThenInclude(e => e.User)
                .ThenInclude(u => u.TagUsers)
                .ThenInclude(tu => tu.Tag)
            .FirstOrDefault(e => e.Id == electionId);
        }

        public List<Tag> GetTags()
        {
            return [.. _context.Tags];
        }
        public List<TagUser> GetTagUsers()
        {
            return [.. _context.TagUsers.Include(tu => tu.Tag)];
        }

        public long GetGolputUsersCount()
        {
            return _context.Users
                .Where(user =>
                    user.DeletedAt == null &&
                    _context.ElectionUsers
                        .Any(eu => eu.UserId == user.Id && eu.ElectionId == _electionId) && // Filter pengguna yang terdaftar di pemilihan
                    !_context.CandidateUsers
                        .Any(cu => cu.UserId == user.Id && cu.Candidate.ElectionId == _electionId)) // Filter pengguna yang belum memilih kandidat
                .Count();
        }
        public override List<User> GetEntries(string search, int page, string orderBy, bool isAsc)
        {
            if (!ModelHelper.IsPropertyExist<User>(orderBy))
            {
                orderBy = "Name";
            }

            page = page < 1 ? 1 : page;

            IQueryable<User> query = _context.Users
                .Include(user => user.TagUsers)
                .Where(user =>
                    user.DeletedAt == null &&
                    _context.ElectionUsers.Any(eu => eu.UserId == user.Id && eu.ElectionId == _electionId) &&
                    !_context.CandidateUsers.Any(cu => cu.UserId == user.Id && cu.Candidate.ElectionId == _electionId));

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(user =>
                    user.Name.ToUpper().Contains(search.ToUpper()) || user.Code.Contains(search));
            }

            if (_selectedTags.Count > 0)
            {
                query = query.Where(user =>
                    user.TagUsers.Any(tagUser => _selectedTags.Contains(tagUser.TagId)));
            }

            query = isAsc
                ? query.OrderBy(user => EF.Property<object>(user, orderBy))
                : query.OrderByDescending(user => EF.Property<object>(user, orderBy));

            query = query.Skip((page - 1) * LimitEntry).Take(LimitEntry);

            return query.ToList();
        }

        public override int GetTotalEntry(string search)
        {
            IQueryable<User> query = _context.Users
                .Include(user => user.TagUsers)
                .Where(user =>
                    user.DeletedAt == null &&
                    _context.ElectionUsers.Any(eu => eu.UserId == user.Id && eu.ElectionId == _electionId) &&
                    !_context.CandidateUsers.Any(cu => cu.UserId == user.Id && cu.Candidate.ElectionId == _electionId));

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(user =>
                    user.Name.ToUpper().Contains(search.ToUpper()) || user.Code.Contains(search));
            }

            if (_selectedTags.Count > 0)
            {
                query = query.Where(user =>
                    user.TagUsers.Any(tagUser => _selectedTags.Contains(tagUser.TagId)));
            }

            return query.Count();
        }

    }
}