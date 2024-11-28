using Microsoft.EntityFrameworkCore;
using PEMIRA.Models;

namespace PEMIRA.Services
{
  public class MonitoringService(DatabaseContext context)
  {
    private readonly DatabaseContext _context = context;

    public List<Election> GetElections()
    {
      return [.. _context.Elections.Where(e => e.DeletedAt == null)];
    }

    public Election? GetElection(long id)
    {
      return _context.Elections
        .Include(e => e.Candidates.Where(c => c.DeletedAt == null).OrderByDescending(c => c.CandidateUsers.Count))
          .ThenInclude(c => c.CandidateUsers)
        .Include(e => e.ElectionUsers)
          .ThenInclude(e => e.User)
          .ThenInclude(u => u.TagUsers)
          .ThenInclude(tu => tu.Tag)
      .FirstOrDefault(e => e.Id == id);
    }

    public List<User> GetGolputUsers(long electionId)
    {
      var usersInElection = _context.ElectionUsers
          .Where(eu => eu.ElectionId == electionId)
          .Select(eu => eu.User)
          .ToList();

      var usersWhoVoted = _context.CandidateUsers
          .Where(cu => cu.Candidate.ElectionId == electionId)
          .Select(cu => cu.User)
          .Distinct()
          .ToList();

      var golputUsers = usersInElection
          .Where(user => !usersWhoVoted.Contains(user))
          .ToList();

      return golputUsers;
    }
  }
}