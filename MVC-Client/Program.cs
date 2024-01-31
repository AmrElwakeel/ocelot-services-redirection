using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
 {
     options.Authority = "https://localhost:44386";

     options.ClientId = "mvc-client";
     options.ClientSecret = "secret";
     options.ResponseType = "code id_token";

     options.Scope.Add("openid");
     options.Scope.Add("profile");
     options.Scope.Add("service-one");
     options.Scope.Add("service-two");

     options.SaveTokens = true;
     options.GetClaimsFromUserInfoEndpoint = true;
 });

builder.Services.AddControllersWithViews();

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

app.UseAuthorization();
app.UseAuthentication();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .RequireAuthorization();

app.Run();
