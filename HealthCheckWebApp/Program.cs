using Auth0.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

var cryptoProvider = new RSACryptoServiceProvider();
cryptoProvider.ImportFromPem(File.ReadAllText("app_keys.pem"));
var securityKey = new RsaSecurityKey(cryptoProvider);

builder.Services
    .AddAuth0WebAppAuthentication(options => {
      options.Domain = builder.Configuration["Auth0:Domain"];
      options.ClientId = builder.Configuration["Auth0:ClientId"];
      options.ClientAssertionSecurityKey = securityKey;
      options.Scope = "openid profile email";
    })
    .WithAccessToken(options =>
      {
          options.Audience = builder.Configuration["Auth0:Audience"];
      });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenHandler>();

builder.Services.AddHttpClient("HealthCheckApi", 
      client => client.BaseAddress = new Uri(builder.Configuration["HealthCheckApiBaseUrl"]))
      .AddHttpMessageHandler<TokenHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
  .CreateClient("HealthCheckApi"));

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

app.Run();
