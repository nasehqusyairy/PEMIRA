using PEMIRA.Interfaces;

namespace PEMIRA.ViewModels
{
  public abstract class TableViewModel : ITableViewModel
  {
    public int PageCount { get; set; } = 1;

    public int CurrentPage { get; set; } = 1;

    public int LimitEntry { get; set; } = 10;

    public string? Search { get; set; }

    public abstract string OrderBy { get; set; }

    public bool IsAsc { get; set; } = true;
  }
}