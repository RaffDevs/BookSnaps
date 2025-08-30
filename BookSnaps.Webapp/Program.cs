using BookSnaps.Application.Features.Owner.Commands.Create;
using BookSnaps.Domain.Repositories;
using BookSnaps.Infra.Persistence.Context;
using BookSnaps.Infra.Persistence.Repositories;
using Cortex.Mediator.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BookSnapsDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseSqlite(builder.Configuration["Database:ConnectionString"]);
    }
    else if (builder.Environment.IsProduction())
    {
        options.UseNpgsql(builder.Configuration["Database:ConnectionString"]);
    }
    
});
builder.Configuration.GetConnectionString(builder.Configuration["Database:ConnectionString"]);
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BookSnapsDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<OwnerRepository>();
builder.Services.AddScoped<BookRepository>();

builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: [typeof(CreateOwnerCommandHandler)],
    configure: options => { options.AddDefaultBehaviors(); }
);
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.SlidingExpiration = true;
});

builder.Services
    .AddControllersWithViews(options =>
    {
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    })
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Clear();
        
        options.ViewLocationFormats.Add("/Views/Pages/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Partials/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Layouts/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
        
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Library}/{action=Index}/{id?}");

if (Convert.ToBoolean(builder.Configuration["Database:Migrate"]))
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<BookSnapsDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Cannot migrate database: ${ex.Message}");
    }
}



app.Run();