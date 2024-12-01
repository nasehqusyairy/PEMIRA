using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.Models;

namespace PEMIRA.Services
{
    public class MenuService(DatabaseContext context, int limit = 10) : TableService<Menu>(limit)
    {
        private readonly DatabaseContext _context = context;
        public override List<Menu> GetEntries(string search, int page, string orderBy, bool isAsc)
        {
            if (!ModelHelper.IsPropertyExist<Menu>(orderBy))
            {
                orderBy = "Name";
            }
            page = page < 1 ? 1 : page;
            IQueryable<Menu> query = _context.Menus.Include(menu => menu.Menusegment)
                      .Where(menu => menu.Name.Contains(search));

            query = isAsc ? query.OrderBy(menu => EF.Property<object>(menu, orderBy)) : query.OrderByDescending(menu => EF.Property<object>(menu, orderBy));
            query = query.Skip((page - 1) * LimitEntry).Take(LimitEntry);
            return [.. query];
        }
        public override int GetTotalEntry(string search) => _context.Menus.Count(menu => menu.Name.Contains(search));

    }
}