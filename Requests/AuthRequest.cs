using Microsoft.EntityFrameworkCore;
using PEMIRA.Interfaces;
using PEMIRA.Models;
using PEMIRA.Services;
using PEMIRA.ViewModels;
using System.Text.RegularExpressions;

namespace PEMIRA.Requests;

public class AuthRequest(DatabaseContext context, AuthViewModel input, AuthService service) : IRequest<AuthViewModel>
{
    public DatabaseContext DBContext { get; set; } = context;
    public AuthViewModel UserInput { get; set; } = input;

    private readonly AuthService _service = service;

    public List<string> GetErrorMessages()
    {
        List<string> errorMessages = [];
        // periksa apakah code dan password diisi
        if (!string.IsNullOrWhiteSpace(UserInput.Code))
        {
            if (!string.IsNullOrEmpty(UserInput.Password))
            {
                // pastikan code hanya berisi huruf dan angka
                if (Regex.IsMatch(UserInput.Code, @"^[a-zA-Z0-9]+$"))
                {
                    if (Regex.IsMatch(UserInput.Password, @"^[a-zA-Z0-9]+$"))
                    {
                        // cari user berdasarkan code
                        User? user = _service.GetUserByCode(UserInput.Code, UserInput.ElectionId);
                        if (user != null)
                        {
                            if (user.Password == UserInput.Password)
                            {
                                // jika id user = 1, maka user adalah admin
                                if (user.Id != 1)
                                {
                                    // cari role user berdasarkan id user dan id pemilihan
                                    RoleUser? roleUser = user.RoleUsers.FirstOrDefault();
                                    if (roleUser != null)
                                    {
                                        return [];
                                    }
                                    else
                                    {
                                        errorMessages.Add("Kamu tidak memiliki akses ke pemilihan ini");
                                    }
                                }
                            }
                            else
                            {
                                errorMessages.Add("Password salah");
                            }
                        }
                        else
                        {
                            errorMessages.Add("User tidak ditemukan");
                        }
                    }
                    else
                    {
                        errorMessages.Add("Password Hanya Boleh Berisi Huruf dan Angka");
                    }
                }
                else
                {
                    errorMessages.Add("ID Hanya Boleh Berisi Huruf dan Angka");
                }
            }
            else
            {
                errorMessages.Add("Password harus diisi");
            }
        }
        else
        {
            errorMessages.Add("ID harus diisi");
        }

        return errorMessages;
    }
}


