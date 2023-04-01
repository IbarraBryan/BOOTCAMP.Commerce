using Api.Gateway.WebClient.Proxy;
using Api.Gateway.WebClient.Proxy.Config;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => { policy.WithOrigins("http://localhost"); });
});
// Proxies
builder.Services.AddSingleton(new ApiGatewayUrl(builder.Configuration.GetValue<string>("ApiGatewayUrl")));
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IOrderProxy, OrderProxy>();
builder.Services.AddHttpClient<IProductProxy, ProductProxy>();
builder.Services.AddHttpClient<IClientProxy, ClientProxy>();

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

app.UseCors();

app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(endpoint => { 
    endpoint.MapRazorPages();
    endpoint.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
});

app.Run();
