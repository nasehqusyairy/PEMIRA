using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PEMIRA.Helpers
{
  public class RequestHelper
  {
    public static List<string> GetErrorMessages(ModelStateDictionary ModelState)
      => [.. ModelState
        .Where(ms => ms.Value?.Errors.Any() == true)
        .SelectMany(ms => ms.Value?.Errors ?? Enumerable.Empty<ModelError>())
        .Select(e => e.ErrorMessage)];
  }
}
