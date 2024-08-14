using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PEMIRA.Interfaces;
using PEMIRA.Models;
using PEMIRA.Services;
using PEMIRA.ViewModels;
using System.Text.RegularExpressions;

namespace PEMIRA.Requests;

public class AuthRequest(ModelStateDictionary modelState, AuthViewModel input, AuthService service) : IRequest<AuthViewModel, AuthService>
{
    public Dictionary<string, dynamic> DerivedData { get; set; } = [];

    public ModelStateDictionary ModelState { get; set; } = modelState;


    public AuthViewModel UserInput { get; set; } = input;

    public AuthService Service { get; set; } = service;

    public bool Validate()
    {
        // periksa apakah user ditemukan, password benar, dan memiliki akses ke pemilihan
        User? user = UserInput.Code != null ? Service.GetUserByCode(UserInput.Code, UserInput.ElectionId) : null;
        if (user == null)
        {
            ModelState.AddModelError("Code", "User tidak ditemukan");
        }
        else if(user.DeletedAt != null)
        {
            ModelState.AddModelError("Code", "User Sudah Tidak Aktif");
        }
        else if (user.Password != UserInput.Password)
        {
            ModelState.AddModelError("Password", "Password salah");
        }
        else if (user.Id != 1)
        {
            RoleUser? roleUser = user.RoleUsers.FirstOrDefault(ru => ru.ElectionId == UserInput.ElectionId);
            if (roleUser == null)
            {
                ModelState.AddModelError("ElectionId", "Anda tidak memiliki akses ke pemilihan ini");
            }
            else
            {
                DerivedData["UserId"] = user.Id;
                DerivedData["RoleId"] = roleUser.RoleId;
            }
        }
        else
        {
            DerivedData["UserId"] = user.Id;
            DerivedData["RoleId"] = 1;
        }

        return ModelState.IsValid;
    }
}
