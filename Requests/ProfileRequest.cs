using Microsoft.AspNetCore.Mvc.ModelBinding;
using PEMIRA.Helpers;
using PEMIRA.Interfaces;
using PEMIRA.ViewModels;
using PEMIRA.Models;
using PEMIRA.Services;

namespace PEMIRA.Requests;

public class ProfileRequest(ModelStateDictionary modelState, ProfileViewModel input, ProfileService service) : IRequest<ProfileViewModel, ProfileService>
{
  public Dictionary<string, dynamic> DerivedData { get; set; } = [];
  public ModelStateDictionary ModelState { get; set; } = modelState;
  public ProfileViewModel UserInput { get; set; } = input;
  public ProfileService Service { get; set; } = service;

  public bool Validate()
  {

    // ID harus unik
    if (UserInput.Code != null && Service.IsCodeUnique(UserInput.Code, UserInput.Id) == false)
    {
      ModelState.AddModelError("Code", "ID sudah digunakan");
    }

    return ModelState.IsValid;
  }

}