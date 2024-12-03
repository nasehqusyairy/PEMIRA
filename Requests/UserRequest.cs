using Microsoft.AspNetCore.Mvc.ModelBinding;
using PEMIRA.Helpers;
using PEMIRA.Interfaces;
using PEMIRA.ViewModels;
using PEMIRA.Models;
using PEMIRA.Services;

namespace PEMIRA.Requests;

public class UserRequest(ModelStateDictionary modelState, UserViewModel input, UserService service) : IRequest<UserViewModel, UserService>
{
    public Dictionary<string, dynamic> DerivedData { get; set; } = [];
    public ModelStateDictionary ModelState { get; set; } = modelState;
    public UserViewModel UserInput { get; set; } = input;
    public UserService Service { get; set; } = service;

    public bool Validate()
    {
        if (Service.IsUserUnique(UserInput.Name, UserInput.Code ?? "") != null)

        {
            ModelState.AddModelError("Code", "Peserta Sudah Ada");
        }
        if (ModelState.IsValid)
        {
            DerivedData["User"] = ModelHelper.MapProperties<UserViewModel, User>(UserInput);
        }
        return ModelState.IsValid;
    }

}