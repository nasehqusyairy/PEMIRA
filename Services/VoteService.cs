using PEMIRA.Models;

namespace PEMIRA.Services
{
  public class VoteService(DatabaseContext context)
  {
    private readonly DatabaseContext _context = context;

    public List<Election> GetElections()
    {
      return [.. _context.Elections];
    }

  }
}