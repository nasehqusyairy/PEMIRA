using McMaster.Extensions.CommandLineUtils;

namespace PEMIRA.Commands
{
  public class CreateViewModelCommand
  {
    public static void Register(CommandLineApplication app)
    {
      app.Command("create:view-model", (command) =>
      {
        command.Description = "Create a view model file with the specified class name";

        command.OnExecute(() =>
          {
            var className = Prompt.GetString("Enter class name: ");
            if (string.IsNullOrEmpty(className))
            {
              Console.WriteLine("Class name is required.");
              return 1;
            }

            var templateFilePath = Path.Combine("Commands/Templates", "ViewModelTemplate.txt");
            if (!File.Exists(templateFilePath))
            {
              Console.WriteLine($"Template file not found: {templateFilePath}");
              return 1;
            }

            var template = File.ReadAllText(templateFilePath);
            template = template.Replace("{ClassName}", className);

            var filePath = Path.Combine("ViewModels", $"{className}.cs");

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

            Console.WriteLine($"Seeder for {className} model has been created successfully at {filePath}");
            return 0;
          });
      });
    }
  }
}
