using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.Models;

namespace PEMIRA.Services
{
    public class MenuRoleService(DatabaseContext context, int limit = 10, long id = 1) : TableService<MenuRole>(limit)
    {
        private readonly DatabaseContext _context = context;
        public override List<MenuRole> GetEntries(string search, int page, string orderBy, bool isAsc)
        {
            page = page < 1 ? 1 : page;

            IQueryable<MenuRole> query = _context.MenuRoles
                .Include(menurole => menurole.Role)
                .Include(menu => menu.Menu)
                .Where(menurole => menurole.MenuId == id && menurole.Role.Name.Contains(search));
            query = isAsc ? query.OrderBy(menurole => menurole.Role.Name) : query.OrderByDescending(menurole => menurole.Role.Name);
            query = query.Skip((page - 1) * LimitEntry).Take(LimitEntry);
            return [.. query];
        }
        public List<Role> GetRoles() => [.. _context.Roles];
        public override int GetTotalEntry(string search) => _context.MenuRoles.Where(menu => menu.MenuId == id).Count(menu => menu.Role.Name.Contains(search));
        public bool IsRoleExist(long id, long menuId) => _context.MenuRoles.Any(role => role.RoleId == id && role.MenuId == menuId);
        public void AddMenuRole (long menuId, long roleId)
        {
            _context.MenuRoles.Add(new MenuRole
            {
                MenuId = menuId,
                RoleId = roleId
            });
            _context.SaveChanges();
        }
    }
}