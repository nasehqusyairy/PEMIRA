using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Services;

public abstract class TableService<T>(int limit) : ITableService<T>
{
  public int LimitEntry { get; set; } = limit;
  public int TotalEntries { get; set; }
  public int TotalFilteredEntries { get; set; }
  public int PageCount { get; set; }

  public abstract List<T> GetEntries(string search, int page, string orderBy, bool isAsc);
  public abstract int GetTotalEntry(string search);
  public void SetEntryCount(string search)
  {
    TotalFilteredEntries = GetTotalEntry(search);
    if (search == "")
    {
      TotalEntries = TotalFilteredEntries;
    }
    else
    {
      TotalEntries = GetTotalEntry("");
    }
    PageCount = (int)Math.Ceiling(TotalFilteredEntries / (double)LimitEntry);
  }
}