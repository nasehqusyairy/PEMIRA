using Microsoft.AspNetCore.Mvc.ModelBinding;
using PEMIRA.Models;

namespace PEMIRA.Requests
{
  public class TagRequest(DatabaseContext context, ModelStateDictionary modelState, Tag input)
  {
    private readonly DatabaseContext _context = context;
    private readonly ModelStateDictionary _modelState = modelState;
    private readonly Tag _input = input;

    public bool Validate()
    {
      if (_input.Name == null)
      {
        _modelState.AddModelError("Name", "Nama tag harus diisi auyauaua");
      }

      return _modelState.IsValid;
    }
  }
}