using Microsoft.AspNetCore.Mvc.ModelBinding;
using PEMIRA.Helpers;
using PEMIRA.Interfaces;
using PEMIRA.ViewModels;
using PEMIRA.Models;
using PEMIRA.Services;

namespace PEMIRA.Requests;

public class ElectionRequest(ModelStateDictionary modelState, ElectionViewModel input, ElectionService service) : IRequest<ElectionViewModel, ElectionService>
{
  public Dictionary<string, dynamic> DerivedData { get; set; } = [];
  public ModelStateDictionary ModelState { get; set; } = modelState;
  public ElectionViewModel UserInput { get; set; } = input;
  public ElectionService Service { get; set; } = service;

  public bool Validate()
  {
        if (Service.IsElectionUnique(UserInput.Name, UserInput.Id) != null)
        {
            ModelState.AddModelError("Name", "Nama pemilihan sudah pernah ada");
        }

        return ModelState.IsValid;
  }

}