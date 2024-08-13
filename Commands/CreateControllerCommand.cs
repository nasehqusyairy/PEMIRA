using McMaster.Extensions.CommandLineUtils;

namespace PEMIRA.Commands
{
  public class CreateControllerCommand
  {
    public static void Register(CommandLineApplication app)
    {
      app.Command("create:controller", (command) =>
      {
        command.Description = "Create a view model file with the specified controller name";

        command.OnExecute(() =>
          {
            var controllerName = Prompt.GetString("Enter controller name: ");
            if (string.IsNullOrEmpty(controllerName))
            {
              Console.WriteLine("Controller name is required.");
              return 1;
            }

            var templateFilePath = Path.Combine("Commands/Templates", "ControllerTemplate.txt");
            if (!File.Exists(templateFilePath))
            {
              Console.WriteLine($"Template file not found: {templateFilePath}");
              return 1;
            }

            var template = File.ReadAllText(templateFilePath);
            template = template.Replace("{ControllerName}", controllerName);

            var filePath = Path.Combine("Controllers", $"{controllerName}Controller.cs");

            if (File.Exists(filePath))
            {
              var overwrite = Prompt.GetYesNo("File already exists. Overwrite?", false);
              if (!overwrite)
              {
                Console.WriteLine("Operation cancelled.");
                return 1;
              }
            }

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            File.WriteAllText(filePath, template);

            Console.WriteLine($"{controllerName}Controller class has been created successfully at {filePath}");
            return 0;
          });
      });
    }
  }
}
