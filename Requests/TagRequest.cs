using Microsoft.AspNetCore.Mvc.ModelBinding;
using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Requests
{
  public class TagRequest(DatabaseContext context, Tag input) : IRequest<Tag>
  {
    public DatabaseContext _context { get; set; } = context;
    public Tag Input { get; set; } = input;

    public List<string> GetErrorMessages()
    {
      List<string> errorMessages = [];

      // logic here

      return errorMessages;
    }

  }
}