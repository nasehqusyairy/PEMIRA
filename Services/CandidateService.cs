using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.Models;
using PEMIRA.ViewModels;

namespace PEMIRA.Services
{
    public class CandidateService(DatabaseContext context, int limit = 10) : TableService<Candidate>(limit)
    {
        private readonly DatabaseContext _context = context;

        public override List<Candidate> GetEntries(string search, int page, string orderBy, bool isAsc)
        {
            // Default to "Name" if the property doesn't exist on the Candidate or User entities
            if (!ModelHelper.IsPropertyExist<Candidate>(orderBy) && orderBy != "User.Name")
            {
                orderBy = "User.Name";
            }

            page = page < 1 ? 1 : page;

            IQueryable<Candidate> query = _context.Candidates
                .Include(candidate => candidate.User)
                .Include(candidate => candidate.Election)
                .Where(candidate => candidate.DeletedAt == null && candidate.User.Name.Contains(search));

            // Dynamic ordering
            if (orderBy == "User.Name")
            {
                query = isAsc
                    ? query.OrderBy(candidate => candidate.User.Name)
                    : query.OrderByDescending(candidate => candidate.User.Name);
            }
            else
            {
                query = isAsc
                    ? query.OrderBy(candidate => EF.Property<object>(candidate, orderBy))
                    : query.OrderByDescending(candidate => EF.Property<object>(candidate, orderBy));
            }

            query = query.Skip((page - 1) * LimitEntry).Take(LimitEntry);

            return query.ToList();
        }

        public override int GetTotalEntry(string search) => _context.Candidates.Count(candidate => candidate.DeletedAt == null && candidate.User.Name.Contains(search));

        public Candidate? IsCandidateUnique(string name, long id) => _context.Candidates.FirstOrDefault(candidate => candidate.User.Name == name && candidate.DeletedAt == null && candidate.Id != id);

        public Candidate? GetCandidate(long id) => _context.Candidates.FirstOrDefault(candidate => candidate.Id == id && candidate.DeletedAt == null);
        public User? GetCandidatebyCode(string code) => _context.Users.FirstOrDefault(candidate => candidate.Code == code);

        public void Store(Candidate candidate, long UserId, long IdUser)
        {
            candidate.CreatedBy = UserId;
            candidate.UserId = IdUser;
            _context.Candidates.Add(candidate);
            _context.SaveChanges();
        }

        public void Update(CandidateViewModel input, Candidate candidate, long UserId)
        {
            ModelHelper.UpdateProperties(input, candidate);
            candidate.UpdatedBy = UserId;
            candidate.UpdatedAt = DateTime.Now;
            _context.Candidates.Update(candidate);
            _context.SaveChanges();
        }

        public void SoftDelete(Candidate candidate, long UserId)
        {
            candidate.DeletedBy = UserId;
            candidate.DeletedAt = DateTime.Now;
            _context.Candidates.Update(candidate);
            _context.SaveChanges();
        }
    }
}
