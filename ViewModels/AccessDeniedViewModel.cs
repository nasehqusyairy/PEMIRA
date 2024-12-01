using System.ComponentModel.DataAnnotations;

namespace PEMIRA.ViewModels
{
  public class AccessDeniedViewModel
  {
    public string Title { get; set; } = "Akses Ditolak";

    public string? ReturnUrl { get; set; }

  }
}