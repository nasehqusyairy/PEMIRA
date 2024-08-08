using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using PEMIRA.Models;
using PEMIRA.Migrations;
namespace PEMIRA.Services
{
    public class AuthService(DatabaseContext context)
    {
        private readonly DatabaseContext _context = context;
    }
}
