using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.Models;
using PEMIRA.ViewModels;

namespace PEMIRA.Services
{
    public class ElectionService(DatabaseContext context, int limit = 10) : TableService<Election>(limit)
    {
        private readonly DatabaseContext _context = context;
        public override List<Election> GetEntries(string search, int page, string orderBy, bool isAsc)
        {
            if (!ModelHelper.IsPropertyExist<Election>(orderBy))
            {
                orderBy = "Name";
            }
            page = page < 1 ? 1 : page;
            IQueryable<Election> query = _context.Elections
                      .Where(election => election.DeletedAt == null && election.Name.Contains(search));

            query = isAsc ? query.OrderBy(election => EF.Property<object>(election, orderBy)) : query.OrderByDescending(election => EF.Property<object>(election, orderBy));
            query = query.Skip((page - 1) * LimitEntry).Take(LimitEntry);
            return [.. query];
        }
        public override int GetTotalEntry(string search) => _context.Elections.Count(election => election.DeletedAt == null && election.Name.Contains(search));
        public Election? GetElection(long id) => _context.Elections.FirstOrDefault(el => el.Id == id);
        public Election? IsElectionUnique(string name, long id) => _context.Elections.FirstOrDefault(el => el.Name == name && el.DeletedAt == null && el.Id != id);
        public bool IsEnrolled(long userid, long electionid) => _context.ElectionUsers.Any(eluser => eluser.UserId == userid && eluser.ElectionId == electionid);
        public void Store(Election election, long UserId)
        {
            election.CreatedBy = UserId;
            _context.Elections.Add(election);
            _context.SaveChanges();
        }
        public void Update(ElectionViewModel input, Election election, long UserId)
        {
            ModelHelper.UpdateProperties(input, election);
            election.UpdatedBy = UserId;
            election.UpdatedAt = DateTime.Now;
            _context.Elections.Update(election);
            _context.SaveChanges();
        }

        public void SoftDelete(Election election, long UserId)
        {
            election.DeletedBy = UserId;
            election.DeletedAt = DateTime.Now;
            _context.Elections.Update(election);
            _context.SaveChanges();
        }

        public List<long> GetParticipantIds(long electionId)
        {
            return [.. _context.ElectionUsers.Where(eluser => eluser.ElectionId == electionId).Select(eluser => eluser.UserId)];
        }
    }
}