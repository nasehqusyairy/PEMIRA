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
    public string? ErrorMessage { get; set; }

    public bool Validate()
    {

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
        User? user = Service.GetCandidatebyCode(UserInput.Code);
        if (user != null)
        {
            Candidate? candidate = Service.GetCandidatebyUserId(user.Id);
            if (candidate != null)
            {
                ErrorMessage = user.Name + " Sudah Terdaftar di " + candidate.Election.Name;
                return false;
            }
        }
        if (UserInput.Image != null)
        {
            using (var image = SixLabors.ImageSharp.Image.Load(UserInput.Image.OpenReadStream()))
            {
                if (image.Width != image.Height)
                {
                    ModelState.AddModelError("Image", "Gambar harus memiliki rasio 1:1.");
                    return false;
                }
            }
        }

        return ModelState.IsValid;
    }

}