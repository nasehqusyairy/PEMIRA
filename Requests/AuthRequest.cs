using Microsoft.EntityFrameworkCore;
using PEMIRA.Interfaces;
using PEMIRA.Models;
using PEMIRA.Services;
using PEMIRA.ViewModels;
using System.Text.RegularExpressions;

namespace PEMIRA.Requests;

public class AuthRequest(AuthViewModel input, AuthService service) : IRequest<AuthViewModel>
{
    public User ValidatedData { get; set; } = new(); 
    public AuthViewModel UserInput { get; set; } = input;
    private readonly AuthService _service = service;

    public List<string> GetErrorMessages()
    {
        List<string> errorMessages = [];

        // periksa apakah kode sudah diisi dan sesuai format
        if (string.IsNullOrWhiteSpace(UserInput.Code))
        {
            errorMessages.Add("ID harus diisi");
        }
        else if (!Regex.IsMatch(UserInput.Code, @"^[a-zA-Z0-9]+$"))
        {
            errorMessages.Add("ID hanya boleh berisi huruf dan angka");
        }

        // periksa apakah kode dan password sudah diisi dan sesuai format
        if (string.IsNullOrWhiteSpace(UserInput.Password))
        {
            errorMessages.Add("Password harus diisi");
        }
        else if (!Regex.IsMatch(UserInput.Password, @"^[a-zA-Z0-9]+$"))
        {
            errorMessages.Add("Password hanya boleh berisi huruf dan angka");
        }

        // periksa apakah user ditemukan, password benar, dan memiliki akses ke pemilihan
        User? user = _service.GetUserByCode(UserInput.Code, UserInput.ElectionId);
        if (user == null)
        {
            errorMessages.Add("User tidak ditemukan");
        }
        else if (user.Password != UserInput.Password)
        {
            errorMessages.Add("Password salah");
        }
        else if (user.Id != 1)
        {
            RoleUser? roleUser = user.RoleUsers.FirstOrDefault();
            if (roleUser == null)
            {
                errorMessages.Add("Kamu tidak memiliki akses ke pencoblosan ini");
            }
            else
            {
                ValidatedData = user;
            }
        }

        return errorMessages;
    }
}
