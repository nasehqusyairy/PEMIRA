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

    // periksa apakah ID pengguna sudah ada
    if (!Service.IsUserExists(UserInput.Code))
    {
      ModelState.AddModelError("Code", "ID pengguna tidak ditemukan");
    }

    // periksa apakah ID pemilihan sudah ada
    if (!Service.IsElectionExists(UserInput.ElectionId))
    {
      ModelState.AddModelError("ElectionId", "ID pemilihan tidak ditemukan");
    }


    // Set derived data if exist
    // if (ModelState.IsValid)
    // {
    //   
    // }

    return ModelState.IsValid;
  }

}