using Microsoft.AspNetCore.Mvc.ModelBinding;
using PEMIRA.Helpers;
using PEMIRA.Interfaces;
using PEMIRA.ViewModels;
using PEMIRA.Models;
using PEMIRA.Services;

namespace PEMIRA.Requests;

public class CandidateRequest(ModelStateDictionary modelState, CandidateViewModel input, CandidateService service) : IRequest<CandidateViewModel, CandidateService>
{
  public Dictionary<string, dynamic> DerivedData { get; set; } = [];
  public ModelStateDictionary ModelState { get; set; } = modelState;
  public CandidateViewModel UserInput { get; set; } = input;
  public CandidateService Service { get; set; } = service;

  public bool Validate()
  {
    // Logic here
    // if (condition)
    // {
    //  ModelState.AddModelError("Keyname", "Message here...");
    // }

    // Set derived data if exist
    // if (ModelState.IsValid)
    // {
    //   
    // }

    return ModelState.IsValid;
  }

}