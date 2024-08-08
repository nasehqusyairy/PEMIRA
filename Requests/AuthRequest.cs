using System.Text.RegularExpressions;
using PEMIRA.Models;

namespace PEMIRA.Requests
{
  public class AuthRequest(DatabaseContext context, User input)
  {
    private readonly DatabaseContext _context = context;
    private readonly User _input = input;

    public List<string> GetErrorMessages()
    {
      List<string> errorMessages = [];
      // periksa apakah code dan password diisi
      if (!string.IsNullOrWhiteSpace(_input.Code))
      {
        if (_input.Password != null && _input.Password != "")
        {
          // pastikan code hanya berisi huruf dan angka
          if (!Regex.IsMatch(_input.Code, @"^[a-zA-Z0-9]+$"))
          {
            errorMessages.Add("Code hanya boleh berisi huruf dan angka");
          }

          if (!Regex.IsMatch(_input.Password, @"^[a-zA-Z0-9]+$"))
          {
            errorMessages.Add("Password hanya boleh berisi huruf dan angka");
          }
        }
        else
        {
          errorMessages.Add("Password harus diisi");
        }
      }
      else
      {
        errorMessages.Add("Code harus diisi");
      }


      // periksa apakah user sudah terdaftar
      var user = _context.Users.FirstOrDefault(user => user.Code == _input.Code);
      if (user == null)
      {
        errorMessages.Add("User tidak ditemukan");
      }
      else if (user.Password != _input.Password)
      {
        errorMessages.Add("Password salah");
      }

      return errorMessages;
    }
  }
}