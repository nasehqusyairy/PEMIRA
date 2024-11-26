using Microsoft.EntityFrameworkCore;
using PEMIRA.Models;

namespace PEMIRA.Services
{
    public class VoteService(DatabaseContext context)
    {
        private readonly DatabaseContext _context = context;

        public List<Election> GetElections()
        {
            return [.. _context.Elections.Where(e => e.DeletedAt == null)];
        }
        public Election? GetElection(long id) => _context.Elections.FirstOrDefault(el => el.Id == id);
        public bool IsUserHasVoted(long id, long electionid) => _context.CandidateUsers.Include(c => c.Candidate).Any(cu => cu.UserId == id && cu.Candidate.ElectionId == electionid);
        public List<Candidate> GetCandidates(long ElectionId)
        {
            return [.. _context.Candidates
      .Include(c => c.User)
      .ThenInclude(u => u.TagUsers)
      .ThenInclude(tu => tu.Tag)
      .Where(c => c.ElectionId == ElectionId && c.DeletedAt == null)];
        }

        public void Store (long userId, long candidateId)
        {
            CandidateUser? choice = new()
            {
                UserId = userId,
                CandidateId = candidateId
            };
            _context.CandidateUsers.Add(choice);
            _context.SaveChanges();
        }

    }
}