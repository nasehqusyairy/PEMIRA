
# PEMIRA Project
This is the PEMIRA project built with ASP.NET Core MVC and Entity Framework.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

- [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

## Running the Project

### Using Visual Studio Code

1.  **Clone the repository:**

```powerShell
git clone "https://github.com/nasehqusyairy/PEMIRA.git"
cd PEMIRA
```

2.  **Open the project in Visual Studio Code:**

```powerShell
code .
```

3.  **Install the necessary extension**

- [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

- [NuGet Package Manager](https://marketplace.visualstudio.com/items?itemName=jmrog.vscode-nuget-package-manager)

4.  **Restore the NuGet Packages**
Open the terminal in VS Code (Ctrl + `) and run:

```powerShell
dotnet restore
```

5.  **Update database**
Run the following command to apply migrations:
```bash
dotnet ef database update
```

6.  **Run the Project**
```bash
dotnet run
```
### Using Visual Studio 2022

1.  **Clone the repository**

```powerShell
git clone "https://github.com/nasehqusyairy/PEMIRA.git"
cd PEMIRA
```

2.  **Open the project in Visual Studio 2022:**
Open `PEMIRA.sln` file

3.  **Restore the NuGet packages:**
Visual Studio will prompt you to restore the packages. Click ``Restore`` to install them.

4.  **Update the dataase**
Open the ``Package Manager Console`` from ``Tools`` > ``NuGet Package Manager`` and run:

```bash
Update-Database
```

5.  **Run the project**
Press `F5` to start debugging the project.

## Commands CLI
We recommend using the terminal instead of the Package Manager Console. Theese commmands are created using `McMaster.Extensions.CommandLineUtils` library. You can modify the commands in the `Commands` Folder. You can also use the `--no-build` option at the end of each command to prevent the terminal from building the project and avoid unnecessary errors.

### Create Seeder
Seeder class is used to provide data automatically to the database. It will represent a table in the database and requires a model to access the table. Once a seeder has been created, you must add it to the `Seeder` array in the `DatabaseSeeder` class in the `Seeder` folder.

You also need to uncomment this line in the `Program.cs` file
```c#
// Uncomment this line to revert the database
// seeder.Revert();
```
Once everything is ready, do a build on the project to empty the data in the table and fill it again with data from the seeder that has been created

Command:
```PowerShell
dotnet run create-seeder
```
This command will display a prompt asking us to enter the class name of the model in question.

Example:
```PowerShell
Enter model name:	User
```
It will create a file named `UserSeeder.cs` in `Seeders` folder. You can also use the [Faker.Net](https://github.com/oriches/faker-cs) library to compose various data

### Create Form Request Validator
We separated the logic for validating user input into a separate file in the `Requests ` folder. This may not be the best way, but it is enough to solve our problem of data validation flexibly.

Command:
```PowerShell
dotnet run create-form-request-validator
```