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

                var modelName = Prompt.GetString("Enter model/viewmodel name: ");
                if (string.IsNullOrEmpty(modelName))
                {
                  Console.WriteLine("Model name is required.");
                  return 1;
                }

                var needsService = Prompt.GetYesNo("Does this class require a service?: ", false);
                var serviceName = string.Empty;
                var serviceType = string.Empty;
                var serviceParameter = string.Empty;
                var serviceProperty = string.Empty;

                if (needsService)
                {
                  serviceName = Prompt.GetString("Enter service class name: ");
                  if (string.IsNullOrEmpty(serviceName))
                  {
                    Console.WriteLine("Service is required.");
                    return 1;
                  }
                  serviceType = $", {serviceName}";
                  serviceParameter = $", {serviceName} service";
                  serviceProperty = $"public {serviceName} Service = service;";
                }

                var templateFilePath = Path.Combine("Commands/Templates", "RequestTemplate.txt");
                if (!File.Exists(templateFilePath))
                {
                  Console.WriteLine($"Template file not found: {templateFilePath}");
                  return 1;
                }

                var template = File.ReadAllText(templateFilePath);
                template = template.Replace("{ClassName}", className);
                template = template.Replace("{InputModel}", modelName);
                template = template.Replace("{ServiceType}", serviceType);
                template = template.Replace("{ServiceParameter}", serviceParameter);
                template = template.Replace("{ServiceProperty}", serviceProperty);

                var filePath = Path.Combine("Requests", $"{className}.cs");

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

                Console.WriteLine($"{className} class has been created successfully at {filePath}");
                return 0;
              });
      });
    }
  }
}
