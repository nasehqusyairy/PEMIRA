using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.Models;
using PEMIRA.ViewModels;

namespace PEMIRA.Services
{
  public class TagService(DatabaseContext context, int limit = 10)
  {
    public int LimitEntry { get; set; } = limit;

    private readonly DatabaseContext _context = context;

    public List<Tag> GetTags(string search, int page, string orderBy, bool isAsc)
    {
      if (!ModelHelper.IsPropertyExist<Tag>(orderBy))
      {
        orderBy = "Name";
      }
      page = page < 1 ? 1 : page;
      IQueryable<Tag> query = _context.Tags
                .Where(tag => tag.DeletedAt == null && tag.Name.Contains(search));

      query = isAsc ? query.OrderBy(tag => EF.Property<object>(tag, orderBy)) : query.OrderByDescending(tag => EF.Property<object>(tag, orderBy));
      query = query.Skip((page - 1) * LimitEntry).Take(LimitEntry);
      return [.. query];
    }

    public int GetPageCount(string search)
    {
      return (int)Math.Ceiling(_context.Tags.Count(tag => tag.DeletedAt == null && tag.Name.Contains(search)) / (double)LimitEntry);
    }

    public Tag? IsTagUnique(string name, long id) => _context.Tags.FirstOrDefault(tag => tag.Name == name && tag.DeletedAt == null && tag.Id != id);

    public Tag? GetTag(long id) => _context.Tags.FirstOrDefault(tag => tag.Id == id && tag.DeletedAt == null);

    public void Store(Tag tag)
    {
      tag.CreatedAt = DateTime.Now;
      tag.UpdatedAt = DateTime.Now;
      _context.Tags.Add(tag);
      _context.SaveChanges();
    }

    public void Update(TagsViewModel input, Tag tag)
    {
      ModelHelper.UpdateProperties(input, tag);
      tag.UpdatedAt = DateTime.Now;
      _context.Tags.Update(tag);
      _context.SaveChanges();
    }

    public void SoftDelete(Tag tag)
    {
      tag.DeletedAt = DateTime.Now;
      _context.Tags.Update(tag);
      _context.SaveChanges();
    }
  }
}