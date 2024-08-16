using System.Collections.Specialized;
using System.Web;

namespace PEMIRA.Helpers;

public class UrlHelper
{

  public static NameValueCollection GetQueryValues(string url)
  {
    var uriBuilder = new UriBuilder(url);
    return HttpUtility.ParseQueryString(uriBuilder.Query);
  }

  // fungsi untuk mengambil nilai dari query string berdasarkan key yang diberikan
  public static string GetQueryValue(string url, string key)
  {
    return GetQueryValues(url)[key] ?? string.Empty;
  }
  // fungsi untuk menambahkan key baru pada query string atau mengubah value dari key yang sudah ada
  public static string AddOrUpdateQueryString(string url, string key, string value)
  {
    var uriBuilder = new UriBuilder(url);
    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
    query[key] = value;
    uriBuilder.Query = query.ToString();
    return uriBuilder.ToString();
  }

  // fungsi untuk order by pada query string berdasarkan key yang diberikan dan mengubah value isAsc dengan memanfaatkan AddOrUpdateQueryString
  public static string SetOrderBy(string url, string key)
  {
    string orderBy = GetQueryValue(url, "orderBy");
    string isAsc = GetQueryValue(url, "isAsc");
    if (orderBy == key)
    {
      isAsc = isAsc == "true" ? "false" : "true";
    }
    else
    {
      isAsc = "true";
    }
    url = AddOrUpdateQueryString(url, "orderBy", key);
    url = AddOrUpdateQueryString(url, "isAsc", isAsc);
    return url;
  }

  // fungsi untuk tombol next pada pagination
  public static string NextPage(string url, int currentPage, int pageCount)
  {
    // jika currentPage sudah mencapai pageCount maka tidak akan ada next page
    if (currentPage >= pageCount)
    {
      return url;
    }
    var uriBuilder = new UriBuilder(url);
    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
    query["CurrentPage"] = (currentPage + 1).ToString();
    uriBuilder.Query = query.ToString();
    return uriBuilder.ToString();
  }

  // fungsi untuk tombol previous pada pagination
  public static string PreviousPage(string url, int currentPage)
  {
    // jika currentPage sudah mencapai 1 maka tidak akan ada previous page
    if (currentPage <= 1)
    {
      return url;
    }
    var uriBuilder = new UriBuilder(url);
    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
    query["CurrentPage"] = (currentPage - 1).ToString();
    uriBuilder.Query = query.ToString();
    return uriBuilder.ToString();
  }
}