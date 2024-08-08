using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Requests
{
  public class CandidateRequest(DatabaseContext context, Candidate input) : IRequest<Candidate>
  {
    public DatabaseContext _context { get; set; } = context;
    public Candidate Input { get; set; } = input;

    public List<string> GetErrorMessages()
    {
      List<string> errorMessages = [];

      // logic here

      return errorMessages;
    }

  }
}