# PEMIRA Project

This is the PEMIRA project built with ASP.NET Core MVC and Entity Framework.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

## Running the Project

### Using Visual Studio Code

1. **Clone the repository:**

  ```PowerShell
    git clone https://github.com/nasehqusyairy/PEMIRA.git
    cd PEMIRA
  ```

2. **Open the project in Visual Studio Code:**

  ```PowerShell
    code .
  ```
  
3. **Install the necessary extension**

- [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [NuGet Package Manager](https://marketplace.visualstudio.com/items?itemName=jmrog.vscode-nuget-package-manager)

4. **Restore the NuGet Packages**

  Open the terminal in VS Code (Ctrl + `) and run:

  ```PowerShell
    dotnet restore
  ```

5. **Update database**

  Run the following command to apply migrations:
  ```PowerShell
    dotnet ef database update
  ```

6. **Run the Project**

  ```PowerShell
    dotnet run
  ```

### Using Visual Studio 2022

1. **Clone the repository**

  ```PowerShell
    git clone https://github.com/nasehqusyairy/PEMIRA.git
    cd PEMIRA
  ```

2. **Open the project in Visual Studio 2022:**

  Open `PEMIRA.sln` file

3. **Restore the NuGet packages:**

  Visual Studio will prompt you to restore the packages. Click ``Restore`` to install them.

4. **Update the dataase**

   Open the ``Package Manager Console`` from ``Tools`` > ``NuGet Package Manager`` and run:
   ```PowerShell
    Update-Database
   ```

5. **Run the project**

  Press ``F5`` to start debugging the project.