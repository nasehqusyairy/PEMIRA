using PEMIRA.Models;

namespace PEMIRA.Services
{
  public class TagService(DatabaseContext context)
  {
    private readonly DatabaseContext _context = context;

    public List<Tag> GetTags() => [.. _context.Tags];

    public void Store(Tag tag)
    {
      _context.Tags.Add(tag);
      _context.SaveChanges();
    }
  }
}