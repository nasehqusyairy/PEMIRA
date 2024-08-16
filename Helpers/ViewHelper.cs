using Microsoft.AspNetCore.Html;

namespace PEMIRA.Helpers;

public class ViewHelper
{
  public static IHtmlContent GenerateSearchAndLimitForm(int limitEntry, string searchQuery)
  {
    var htmlContent = $@"
            <div class='input-group mb-3'>
                <input type='number' class='form-control' name='LimitEntry' value='{limitEntry}'
                    placeholder='Jumlah baris per halaman...(max 100)' max='100'>
                <input type='text' class='form-control' name='Search' value='{searchQuery}' 
                    placeholder='Cari penanda...'>
                <button class='btn btn-primary' type='submit'><i class='bi bi-search'></i></button>
            </div>";

    return new HtmlString(htmlContent);
  }

  public static IHtmlContent GeneratePagination(string currentUrl, int currentPage, int pageCount)
  {
    pageCount = pageCount < 1 ? 1 : pageCount;
    currentPage = currentPage < 1 ? 1 : currentPage > pageCount ? pageCount : currentPage;
    var previousPageUrl = UrlHelper.PreviousPage(currentUrl, currentPage);
    var nextPageUrl = UrlHelper.NextPage(currentUrl, currentPage, pageCount);

    var paginationHtml = $@"
            <div class='d-flex align-items-center justify-content-end'>
                <span>Halaman</span>
                <div class='mx-1'>
                    <div class='input-group'>
                        <button type='button' class='btn btn-primary'
                            onclick=""window.location.href='{previousPageUrl}'"" {(currentPage == 1 ? "disabled" : "")}>
                            <i class='bi bi-chevron-left'></i>
                        </button>

                        <input type='number' class='form-control text-center' name='CurrentPage' value='{currentPage}' min='1' max='{pageCount}'>

                        <button type='button' class='btn btn-primary'
                            onclick=""window.location.href='{nextPageUrl}'"" {(currentPage == pageCount ? "disabled" : "")}>
                            <i class='bi bi-chevron-right'></i>
                        </button>
                    </div>
                </div>
                <span>dari {pageCount}</span>
            </div>";

    return new HtmlString(paginationHtml);
  }

  public static IHtmlContent GenerateNoDataRow(int colspan, string message = "Data tidak ditemukan")
  {
    var htmlContent = $@"
            <tr>
                <td colspan='{colspan}' class='text-center'>{message}</td>
            </tr>";

    return new HtmlString(htmlContent);
  }


}