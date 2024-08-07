using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;

namespace PEMIRA.Commands
{
  public class CreateSeederCommand
  {
    public static void Register(CommandLineApplication app)
    {
      app.Command("create-seeder", (command) =>
      {
        command.Description = "Create a seeder file with the specified model name";

        command.OnExecute(() =>
          {
            var modelName = Prompt.GetString("Enter model name: ");
            if (string.IsNullOrEmpty(modelName))
            {
              Console.WriteLine("Model name is required.");
              return 1;
            }

            var templateFilePath = Path.Combine("Commands/Templates", "SeederTemplate.txt");
            if (!File.Exists(templateFilePath))
            {
              Console.WriteLine($"Template file not found: {templateFilePath}");
              return 1;
            }

            var template = File.ReadAllText(templateFilePath);
            template = template.Replace("{Model}", modelName);

            var filePath = Path.Combine("Seeders", $"{modelName}Seeder.cs");
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            File.WriteAllText(filePath, template);

            Console.WriteLine($"Seeder for {modelName} created successfully at {filePath}");
            return 0;
          });
      });
    }
  }
}
