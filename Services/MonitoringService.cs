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
        .Include(e => e.Candidates.Where(c => c.DeletedAt == null))
          .ThenInclude(c => c.CandidateUsers)
        .Include(e => e.ElectionUsers)
          .ThenInclude(e => e.User)
      .FirstOrDefault(e => e.Id == id);
    }
  }
}