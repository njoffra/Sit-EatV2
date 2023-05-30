using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SitnEatV2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AuthDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("SitnEatConnectionString"));
}
   );
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();

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
	pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "login",
//    pattern: "login",
//    defaults: new { controller = "Account", action = "Login" });

//app.MapControllerRoute(
//    name: "register",
//    pattern: "register",
//    defaults: new { controller = "Account", action = "Register" });

using(var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	var roles = new[] { "Admin", "User" };

	foreach(var role in roles)
	{
		if(!await roleManager.RoleExistsAsync(role))
		{
			await roleManager.CreateAsync(new IdentityRole(role));
		}
	}
}

async Task Main()
{
    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new[] { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    using (var scope = app.Services.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string email = "admin@admin.com";
        string password = "Admin123_";
        if(await userManager.FindByEmailAsync(email) == null)
        {
            var user = new IdentityUser();

            user.UserName = email;
            user.Email = email;

            await userManager.CreateAsync (user, password);

            await userManager.AddToRoleAsync(user, "Admin");
        }
    }

    app.Run();
}

await Main();
