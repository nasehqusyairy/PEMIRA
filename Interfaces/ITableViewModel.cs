namespace PEMIRA.Interfaces;

public interface ITableViewModel
{
  public int PageCount { get; set; }

  public int CurrentPage { get; set; }

  public int LimitEntry { get; set; }

  public string? Search { get; set; }

  public string OrderBy { get; set; }

  public bool IsAsc { get; set; }
}