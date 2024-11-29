using Microsoft.EntityFrameworkCore;
using PEMIRA.Models;

namespace PEMIRA.Services
{
  public class MonitoringService(DatabaseContext context, int limit, long electionId) : TableService<User>(limit)
  {
    private readonly DatabaseContext _context = context;

    private readonly long electionId = electionId;

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

    public long GetGolputUsersCount()
    {
      return _context.ElectionUsers
        .Where(eu => eu.ElectionId == electionId)
        .Select(eu => eu.User)
        .Count(user => !user.CandidateUsers.Any());
    }

    public override List<User> GetEntries(string search, int page, string orderBy, bool isAsc)
    {
      // var usersInElection = _context.ElectionUsers
      //           .Where(eu => eu.ElectionId == electionId)
      //           .Select(eu => eu.User)
      //           .ToList();

      // var usersWhoVoted = _context.CandidateUsers
      //     .Where(cu => cu.Candidate.ElectionId == electionId)
      //     .Select(cu => cu.User)
      //     .Distinct()
      //     .ToList();

      // var golputUsers = usersInElection
      //     .Where(user => !usersWhoVoted.Contains(user))
      //     .ToList();

      // return golputUsers;
      return [];
    }

    public override int GetTotalEntry(string search)
    {
      return 1;
    }
  }
}