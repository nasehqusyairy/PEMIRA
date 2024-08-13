
# PEMIRA Project
This is the PEMIRA (Pemilihan Umum Raya) project built with ASP.NET Core MVC and Entity Framework.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

- [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

## Running the Project

### Using Visual Studio Code

1.  **Clone the repository:**

```bash
git clone "https://github.com/nasehqusyairy/PEMIRA.git"
cd PEMIRA
```

2.  **Open the project in Visual Studio Code:**

```bash
code .
```

3.  **Install the necessary extension**

- [C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

- [NuGet Package Manager](https://marketplace.visualstudio.com/items?itemName=jmrog.vscode-nuget-package-manager)

4.  **Restore the NuGet Packages**
Open the terminal in VS Code (Ctrl + `) and run:

```bash
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

```bash
git clone "https://github.com/nasehqusyairy/PEMIRA.git"
cd PEMIRA
```

2.  **Open the project in Visual Studio 2022**

3.  **Restore the NuGet packages:**
Visual Studio will prompt you to restore the packages. Click ``Restore`` to install them.

4.  **Update the database**
Open the ``Package Manager Console`` from ``Tools`` > ``NuGet Package Manager`` and run:

```bash
Update-Database
```

5.  **Run the project**
Press `F5` to start debugging the project.

## Commands CLI
We recommend using the terminal instead of the Package Manager Console. Theese commmands are created using [CommandLineUtils](https://github.com/natemcmaster/CommandLineUtils) library. You can modify the commands in the `Commands` Folder. You can also use the `--no-build` option at the end of each command to prevent the terminal from building the project and avoid unnecessary errors.

### Create Seeder
Seeder class is used to provide data automatically to the database. It will represent a table in the database and requires a model to access the table. Once a seeder has been created, you must add it to the `Seeder` array in the `DatabaseSeeder` class in the `Seeder` folder.

You also need to uncomment theese line in the `Program.cs` file
```c#
// seeder.Revert();

// seeder.Seed();
```
Once everything is ready, do a build on the project to empty the data in the table and fill it again with data from the seeder that has been created.

Command:
```bash
dotnet run create:seeder
```
This command will display a prompt asking us to enter the class name of the model in question.

Example:
```
Enter model name: User
```
It will create a file named `UserSeeder.cs` in `Seeders` folder. You can also use the [Faker.Net](https://github.com/oriches/faker-cs) library to compose various data.

### Create View Model
We realize that it is important to create a view model to map data to the view and perform validation on the client side. For this reason, we provide a CLI to make it easier for developers to create view models.

Command:
```bash
dotnet run create:view-model
```
This command will display a prompt asking us to enter the class name of the view model in question.

Example:
```
Enter class name: AuthViewModel
```
It will create a file named `AuthViewModel.cs` in `ViewModels` folder.

We recommend setting up validation on the client side using `System.ComponentModel.DataAnnotations` as in the following example:
```csharp
using System.ComponentModel.DataAnnotations;

namespace PEMIRA.ViewModels;

public class AuthViewModel
{
  [Required(ErrorMessage = "Username is required")]
  [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain letters and numbers")]
  public string? Username { get; set; }
}
```
To make it easier for developers to find validation attribute references, we have summarized them in the table below.

#### Built-in Validation Attributes

| Attribute                                                                                       | Description                                                                                         |
| ------------------------------------------------------------------------------------------------ | --------------------------------------------------------------------------------------------------- |
| [AllowedValuesAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.allowedvaluesattribute?view=net-8.0) | Specifies a list of values that should be allowed in a property.                                    |
| [AssociatedMetadataTypeTypeDescriptionProvider](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.associatedmetadatatypetypedescriptionprovider?view=net-8.0) | Extends the metadata information for a class by adding attributes and property information that is defined in an associated class. |
| [AssociationAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.associationattribute?view=net-8.0) | Specifies that an entity member represents a data relationship, such as a foreign key relationship. |
| [Base64StringAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.base64stringattribute?view=net-8.0) | Specifies that a data field value is a well-formed Base64 string.                                   |
| [BindableTypeAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.bindabletypeattribute?view=net-8.0) | Specifies whether a type is typically used for binding.                                             |
| [CompareAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.compareattribute?view=net-8.0) | Provides an attribute that compares two properties.                                                 |
| [ConcurrencyCheckAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.concurrencycheckattribute?view=net-8.0) | Specifies that a property participates in optimistic concurrency checks.                             |
| [CreditCardAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.creditcardattribute?view=net-8.0) | Specifies that a data field value is a credit card number.                                          |
| [CustomValidationAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.customvalidationattribute?view=net-8.0) | Specifies a custom validation method that is used to validate a property or class instance.         |
| [DataTypeAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.datatypeattribute?view=net-8.0) | Specifies the name of an additional type to associate with a data field.                            |
| [DeniedValuesAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.deniedvaluesattribute?view=net-8.0) | Specifies a list of values that should not be allowed in a property.                                |
| [DisplayAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.displayattribute?view=net-8.0) | Provides a general-purpose attribute that lets you specify localizable strings for types and members of entity partial classes. |
| [DisplayColumnAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.displaycolumnattribute?view=net-8.0) | Specifies the column that is displayed in the referred table as a foreign-key column.               |
| [DisplayFormatAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.displayformatattribute?view=net-8.0) | Specifies how data fields are displayed and formatted by ASP.NET Dynamic Data.                      |
| [EditableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.editableattribute?view=net-8.0) | Indicates whether a data field is editable.                                                         |
| [EmailAddressAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.emailaddressattribute?view=net-8.0) | Validates an email address.                                                                         |
| [EnumDataTypeAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.enumdatatypeattribute?view=net-8.0) | Enables a .NET enumeration to be mapped to a data column.                                           |
| [FileExtensionsAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.fileextensionsattribute?view=net-8.0) | Provides file name extensions that are allowed in a data field.                                     |
| [FilterUIHintAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.filteruihintattribute?view=net-8.0) | Specifies the filtering behavior for a field template.                                              |
| [KeyAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.keyattribute?view=net-8.0) | Denotes one or more properties of an entity class as the entity's unique identity or primary key.    |
| [MaxLengthAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.maxlengthattribute?view=net-8.0) | Specifies the maximum length of array or string data allowed in a property.                         |
| [MinLengthAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.minlengthattribute?view=net-8.0) | Specifies the minimum length of array or string data allowed in a property.                         |
| [PhoneAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.phoneattribute?view=net-8.0) | Validates a phone number.                                                                           |
| [RangeAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.rangeattribute?view=net-8.0) | Validates that a value falls within a specified range.                                              |
| [RegularExpressionAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.regularexpressionattribute?view=net-8.0) | Validates that the value of a property matches a specified regular expression.                      |
| [RequiredAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.requiredattribute?view=net-8.0) | Indicates that a data field value is required.                                                      |
| [ScaffoldColumnAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.scaffoldcolumnattribute?view=net-8.0) | Specifies whether a class or data field is displayed in a view.                                     |
| [StringLengthAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.stringlengthattribute?view=net-8.0) | Specifies the maximum and minimum length of characters that are allowed in a data field.            |
| [TimestampAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.timestampattribute?view=net-8.0) | Marks a property as being a timestamp.                                                              |
| [UIHintAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.uihintattribute?view=net-8.0) | Specifies the name of the user control to use for a data field in a Dynamic Data application.       |
| [UrlAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.urlattribute?view=net-8.0) | Validates that a data field value is a well-formed URL.                                             |
| [ValidationAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationattribute?view=net-8.0) | Serves as the base class for all validation attributes.                                             |
| [ValidationContext](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationcontext?view=net-8.0) | Describes the context in which a validation check is performed.                                     |
| [ValidationException](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationexception?view=net-8.0) | Represents the exception that occurs during validation of a data field when the ValidationAttribute class is used. |
| [ValidationResult](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationresult?view=net-8.0) | Represents the result of validating a data field.                                                   |
| [Validator](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validator?view=net-8.0) | Provides a set of methods that provide a way to validate property values based on validation attributes. |

If you feel the need to validate input on the server side (such as database matching, etc.), you can try using a [Request Validator](#create-request-validator).

### Create Request Validator
We separated the logic for validating user input on the server side into a separate file in the `Requests` folder. This may not be the best way, but it is enough to solve our problem of data validation flexibly.

Command:
```bash
dotnet run create:request-validator
```

This command will display a prompt asking us to enter some required information.

Example:
```
Enter class name: AuthRequest
Enter model/viewmodel name: AuthViewModel
Does this class require a service?:  [y/N] y
Enter service class name: AuthService
```
It will create a file named `AuthRequest.cs` in `Requests` folder.

### Create Controller
We added a BaseController class which can later be inherited from other controllers. The aim is to avoid the same code on most controllers, especially in the constructor section. For this reason, we created a command to make it easier to create a controller that also inherits the BaseController class.

Command:
```bash
dotnet run create:controller
```
This command will display a prompt asking us to enter the name of the controller in question.

Example:
```
Enter controller name: Auth
```
It will create a file named `AuthController.cs` in `Controllers` folder.

By default, we add cookie access to the BaseController class constructor in the form of the `Cookie` property so that all controllers can access cookies through this property for purposes such as reading logged in user data, etc.

### Create Service
To separate complex logic in controller actions (especially for processing data directly related to the database) or just provide initial values ​​to the [View Model](#create-view-model), we recommend that you create another class which we usually call a service class which will separate the logic into methods.

Command:
```bash
dotnet run create:service
```
This command will display a prompt asking us to enter the class name of the service in question.

Example:
```
Enter class name: AuthService
```
It will create a file named `AuthService.cs` in `Services` folder.

## Helpers
Helpers make it easy for us to store functions that can be used globally. All helpers should be created using static methods and stored in a class in the `Helpers` folder.

Here are some helpers that we have provided by default.

### Model Helper
#### `TDestination ModelHelper.MapProperties<TSource, TDestination>(TSource source)`
This method is useful for converting model objects to view model objects or vice versa.

Example:
```csharp
Tag tag = new Tag { Name = "Example Tag" };
TagsViewModel viewModel = tag.MapProperties<Tag, TagsViewModel>();

Console.WriteLine(viewModel.Name); // Output: Example Tag

TagsViewModel anotherViewModel = new TagsViewModel { Name = "New Tag" };
Tag newTag = anotherViewModel.MapProperties<TagsViewModel, Tag>();

Console.WriteLine(newTag.Name); // Output: New Tag
```

#### `void ModelHelper.UpdateProperties<TSource, TTarget>(TSource source, TTarget target)`
This method is useful for changing property values ​​on model objects with property values ​​on view model objects.

Example:
```csharp
// User class Example
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}

// UserViewModel Example
public class UserViewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int? Age { get; set; }
}

// Example using
var user = new User { Id = 1, Name = "John", Email = "john@example.com", Age = 30 };
var userViewModel = new UserViewModel { Name = "Johnny", Email = "  ", Age = 31 };

ModelHelper.UpdateProperties(userViewModel, user);

// After update, user.Name = "Johnny", user.Email = "john@example.com", user.Age = 31
```