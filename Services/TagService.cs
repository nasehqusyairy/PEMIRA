using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.Models;
using PEMIRA.ViewModels;

namespace PEMIRA.Services
{
  public class TagService(DatabaseContext context)
  {
    private readonly DatabaseContext _context = context;


    public Tag? IsTagUnique(string name, long id) => _context.Tags.FirstOrDefault(tag => tag.Name == name && tag.DeletedAt == null && tag.Id != id);

    public Tag? GetTag(long id) => _context.Tags.FirstOrDefault(tag => tag.Id == id && tag.DeletedAt == null);

    public void Store(Tag tag)
    {
      _context.Tags.Add(tag);
      _context.SaveChanges();
    }

    public void Update(TagsViewModel input, Tag tag)
    {
      ModelHelper.UpdateProperties(input, tag);
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