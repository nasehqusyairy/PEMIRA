namespace PEMIRA.Interfaces;

public interface IRequest<IM, S>
{
  object ValidatedData { get; set; }
  IM UserInput { get; set; }

  S Service { get; set; }
  List<string> GetErrorMessages();
}

public interface IRequest<IM>
{
  object ValidatedData { get; set; }
  IM UserInput { get; set; }

  List<string> GetErrorMessages();
}
