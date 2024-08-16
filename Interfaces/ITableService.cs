namespace PEMIRA.Interfaces;

public interface ITableService<T>
{
  public int LimitEntry { get; set; }

  public int TotalEntries { get; set; }

  public int TotalFilteredEntries { get; set; }

  public int PageCount { get; set; }

  public List<T> GetEntries(string search, int page, string orderBy, bool isAsc);

  public void SetEntryCount(string search);

  public int GetTotalEntry(string search);
}