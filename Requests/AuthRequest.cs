using PEMIRA.Interfaces;
using PEMIRA.Models;
using System.Text.RegularExpressions;

namespace PEMIRA.Requests
{
    public class AuthRequest(DatabaseContext context, User input) : IRequest<User>
    {
        public DatabaseContext _context { get; set; } = context;
        public User Input { get; set; } = input;
        private readonly User _input = input;

        public List<string> GetErrorMessages()
        {
            List<string> errorMessages = [];
            // periksa apakah code dan password diisi
            if (!string.IsNullOrWhiteSpace(_input.Code))
            {
                if (!string.IsNullOrEmpty(_input.Password))
                {
                    // pastikan code hanya berisi huruf dan angka
                    if (!Regex.IsMatch(_input.Code, @"^[a-zA-Z0-9]+$"))
                    {
                        errorMessages.Add("ID Hanya Boleh Berisi Huruf dan Angka");
                    }
                    if (!Regex.IsMatch(_input.Password, @"^[a-zA-Z0-9]+$"))
                    {
                        errorMessages.Add("Password Hanya Boleh Berisi Huruf dan Angka");
                    }
                    var user = _context.Users.FirstOrDefault(user => user.Code == _input.Code);
                    if (user == null)
                    {
                        errorMessages.Add("User tidak ditemukan");
                    }
                    else if (user.Password != _input.Password)
                    {
                        errorMessages.Add("Password salah");
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


            // periksa apakah user sudah terdaftar


            return errorMessages;
        }
    }
}

