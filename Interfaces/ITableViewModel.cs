namespace PEMIRA.Interfaces;

public interface ITableViewModel<T>
{
  public int PageCount { get; set; }

  public int CurrentPage { get; set; }

  public int LimitEntry { get; set; }

  public string? Search { get; set; }

  public string OrderBy { get; set; }

  public bool IsAsc { get; set; }

  public int TotalFilteredEntries { get; set; }

  public int TotalEntries { get; set; }

  public List<T> Entries { get; set; }
}