using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages & MVC
builder.Services.AddRazorPages();
builder.Services.AddControllers();

//  Add Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(endpoint => { 
    endpoint.MapRazorPages();
    endpoint.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
});

app.Run();
