using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PEMIRA.Models;

namespace PEMIRA.Requests
{
  public class AuthRequest(DatabaseContext context, ModelStateDictionary modelState, User input)
  {
    private readonly DatabaseContext _context = context;
    private readonly ModelStateDictionary _modelState = modelState;
    private readonly User _input = input;

    public bool Validate()
    {
      // periksa apakah code dan password diisi
      if (string.IsNullOrWhiteSpace(_input.Code))
      {
        _modelState.AddModelError("Code", "Code harus diisi");

        if (_input.Password != null && _input.Password != "")
        {
          _modelState.AddModelError("Password", "Password harus diisi");

          // pastikan code hanya berisi huruf dan angka
          if (!Regex.IsMatch(_input.Code, @"^[a-zA-Z0-9]+$"))
          {
            _modelState.AddModelError("Code", "Code hanya boleh berisi huruf dan angka");
          }

          if (!Regex.IsMatch(_input.Password, @"^[a-zA-Z0-9]+$"))
          {
            _modelState.AddModelError("Password", "Password hanya boleh berisi huruf dan angka");
          }
        }
      }


      // periksa apakah user sudah terdaftar
      var user = _context.Users.FirstOrDefault(user => user.Code == _input.Code);
      if (user == null)
      {
        _modelState.AddModelError("Code", "User tidak ditemukan");
      }
      else if (user.Password != _input.Password)
      {
        _modelState.AddModelError("Password", "Password salah");
      }

      return _modelState.IsValid;
    }
  }
}