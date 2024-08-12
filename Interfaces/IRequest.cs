using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PEMIRA.Interfaces;

public interface IRequest<IM, S>
{
  public Dictionary<string, dynamic> DerivedData { get; set; }
  public ModelStateDictionary ModelState { get; set; }
  public IM UserInput { get; set; }
  public S Service { get; set; }
  public bool Validate();
}

public interface IRequest<IM>
{
  public Dictionary<string, dynamic> DerivedData { get; set; }
  public ModelStateDictionary ModelState { get; set; }
  public IM UserInput { get; set; }
  public bool Validate();
}
