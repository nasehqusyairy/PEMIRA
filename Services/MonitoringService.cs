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
  }
}