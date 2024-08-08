using McMaster.Extensions.CommandLineUtils;
using PEMIRA.Commands;
using PEMIRA.Seeders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var app = new CommandLineApplication
{
	Name = "PEMIRA CLI"
};
app.HelpOption("-?|-h|--help");

CreateSeederCommand.Register(app);
CreateRequestCommand.Register(app);

app.OnExecute(() =>
{
	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.
	builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

	builder.Services.AddScoped<DatabaseSeeder>();

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (!app.Environment.IsDevelopment())
	{
		app.UseExceptionHandler("/Home/Error");
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}
	else
	{
		// Database seeding
		using var scope = app.Services.CreateScope();
		var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();

		// Uncomment this line to revert the database
		seeder.Revert();

		seeder.Seed();
	}

	app.UseHttpsRedirection();
	app.UseStaticFiles();

	app.UseRouting();

	app.UseAuthorization();

	app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

	app.Run();

	return 0;
});

app.Execute(args);
