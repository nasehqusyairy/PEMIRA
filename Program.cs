using McMaster.Extensions.CommandLineUtils;
using PEMIRA.Commands;
using PEMIRA.Seeders;
using Microsoft.AspNetCore.Authentication.Cookies;
var app = new CommandLineApplication
{
    Name = "PEMIRA CLI"
};
app.HelpOption("-?|-h|--help");

CreateSeederCommand.Register(app);
CreateRequestCommand.Register(app);
CreateViewModelCommand.Register(app);

app.OnExecute(() =>
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

    builder.Services.AddScoped<DatabaseSeeder>();
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth"; // Redirect to Login action on unauthorized access
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/AccessDenied"; // Redirect on forbidden actions
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10); // Set cookie expiration

    });
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Admin",
             policy => policy.RequireRole("1"));
        options.AddPolicy("Pembina",
             policy => policy.RequireRole("2"));
        options.AddPolicy("Panitia",
             policy => policy.RequireRole("3"));
        options.AddPolicy("Peserta",
             policy => policy.RequireRole("4"));
    });
    builder.Services.AddSession(options =>
    {
        options.Cookie.Name = ".PEMIRA.Session";
        options.IdleTimeout = TimeSpan.FromMinutes(10);
        options.Cookie.IsEssential = true;
        options.Cookie.HttpOnly = true;
    });
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
    app.UseSession();
    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

    return 0;
});

app.Execute(args);
