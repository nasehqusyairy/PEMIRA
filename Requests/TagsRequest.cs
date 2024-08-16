using Microsoft.AspNetCore.Mvc.ModelBinding;
using PEMIRA.Helpers;
using PEMIRA.Interfaces;
using PEMIRA.Models;
using PEMIRA.Services;
using PEMIRA.ViewModels;

namespace PEMIRA.Requests;

public class TagsRequest(ModelStateDictionary modelState, TagsViewModel input, TagService service) : IRequest<TagsViewModel, TagService>
{
  public Dictionary<string, dynamic> DerivedData { get; set; } = [];
  public ModelStateDictionary ModelState { get; set; } = modelState;
  public TagsViewModel UserInput { get; set; } = input;
  public TagService Service { get; set; } = service;

  public bool Validate()
  {
    // logic here
    if (Service.IsTagUnique(UserInput.Name, UserInput.Id) != null)
    {
      ModelState.AddModelError("Name", "Nama penanda sudah pernah ada");
    }

    return ModelState.IsValid;
  }
}