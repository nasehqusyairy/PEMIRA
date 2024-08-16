using PEMIRA.Interfaces;

namespace PEMIRA.Helpers;

public static class TableHelper
{
  public static int SetLimitEntry(int limit, int max = 100)
  {
    return limit < 1 ? 1 : limit > max ? max : limit;
  }

  public static int SetCurrentPage(int currentPage, int pageCount)
  {
    return currentPage < 1 ? 1 : currentPage > pageCount ? pageCount : currentPage;
  }

  public static void SetTableViewModel<T>(ITableService<T> service, ITableViewModel<T> viewModel)
  {
    service.SetEntryCount(viewModel.Search ?? "");

    viewModel.TotalEntries = service.TotalEntries;
    viewModel.TotalFilteredEntries = service.TotalFilteredEntries;

    int pageCount = service.PageCount;

    viewModel.PageCount = pageCount;
    viewModel.CurrentPage = TableHelper.SetCurrentPage(viewModel.CurrentPage, pageCount);

    viewModel.Entries = service.GetEntries(viewModel.Search ?? "", viewModel.CurrentPage, viewModel.OrderBy, viewModel.IsAsc);
  }
}