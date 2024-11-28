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
        User? user = UserInput.Code != null ? Service.GetUserByCode(UserInput.Code) : null;
        if (user == null)
        {
            ModelState.AddModelError("Code", "User tidak ditemukan");
        }
        else if (user.DeletedAt != null)
        {
            ModelState.AddModelError("Code", "User Sudah Tidak Aktif");
        }
        else if (user.Password != UserInput.Password)
        {
            ModelState.AddModelError("Password", "Password salah");
        }
        else if (user.Id != 1)
        {
            ElectionUser? participant = Service.GetParticipant(user.Id, UserInput.ElectionId);
            RoleUser? roleUser = user.RoleUsers.FirstOrDefault(ru => ru.UserId == user.Id);
            if (participant == null)
            {
                ModelState.AddModelError("ElectionId", "Anda tidak memiliki akses ke pemilihan ini");
            }
            else
            {
                DerivedData["UserId"] = user.Id;
                if (roleUser != null)
                {
                    DerivedData["RoleId"] = roleUser.RoleId;
                }
                else
                {
                    DerivedData["RoleId"] = 4;
                }
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
