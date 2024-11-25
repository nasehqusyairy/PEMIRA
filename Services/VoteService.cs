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

    public List<Candidate> GetCandidates(long ElectionId)
    {
      return [.. _context.Candidates
      .Include(c => c.User)
      .ThenInclude(u => u.TagUsers)
      .ThenInclude(tu => tu.Tag)
      .Where(c => c.ElectionId == ElectionId && c.DeletedAt == null)];
    }

  }
}