using Microsoft.EntityFrameworkCore;
using PEMIRA.Interfaces;
using PEMIRA.Models;
using PEMIRA.Services;
using PEMIRA.ViewModels;
using System.Text.RegularExpressions;

namespace PEMIRA.Requests;

public class AuthRequest(AuthViewModel input, AuthService service) : IRequest<AuthViewModel, AuthService>
{
    public class ValidatedDataObject
    {
        public User User { get; set; } = new();
        public long RoleId { get; set; }
    };

    public object ValidatedData { get; set; } = new ValidatedDataObject();
    public AuthViewModel UserInput { get; set; } = input;

    public AuthService Service { get; set; } = service;

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
        User? user = Service.GetUserByCode(UserInput.Code, UserInput.ElectionId);
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
            RoleUser? roleUser = user.RoleUsers.FirstOrDefault(ru => ru.ElectionId == UserInput.ElectionId);
            if (roleUser == null)
            {
                errorMessages.Add("Kamu tidak memiliki akses ke pencoblosan ini");
            }
            else
            {
                ValidatedData = new ValidatedDataObject()
                {
                    User = user,
                    RoleId = roleUser.RoleId
                };
            }
        }
        else
        {
            ValidatedData = new ValidatedDataObject()
            {
                User = user,
                RoleId = 1
            };
        }

        return errorMessages;
    }
}
