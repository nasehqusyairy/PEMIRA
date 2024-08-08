using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;

namespace PEMIRA.Commands
{
  public class CreateRequestCommand
  {
    public static void Register(CommandLineApplication app)
    {
      app.Command("create-form-request-validator", (command) =>
      {
        command.Description = "Create a form request validator file with the specified name";

        command.OnExecute(() =>
          {
            var className = Prompt.GetString("Enter class name: ");
            if (string.IsNullOrEmpty(className))
            {
              Console.WriteLine("Class name is required.");
              return 1;
            }

            var modelName = Prompt.GetString("Enter model name: ");
            if (string.IsNullOrEmpty(modelName))
            {
              Console.WriteLine("Model name is required.");
              return 1;
            }

            var templateFilePath = Path.Combine("Commands/Templates", "RequestTemplate.txt");
            if (!File.Exists(templateFilePath))
            {
              Console.WriteLine($"Template file not found: {templateFilePath}");
              return 1;
            }

            var template = File.ReadAllText(templateFilePath);
            template = template.Replace("{ClassName}", className);
            template = template.Replace("{Model}", modelName);

            var filePath = Path.Combine("Requests", $"{className}.cs");
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            File.WriteAllText(filePath, template);

            Console.WriteLine($"{className} class has been created successfully at {filePath}");
            return 0;
          });
      });
    }
  }
}
