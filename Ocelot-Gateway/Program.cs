using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);


//For identity server 4
string authenticationProviderKey = "secret";

Action<JwtBearerOptions> options = (opt) =>
{
    opt.Authority = "https://localhost:44386";
    //x.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };
};

builder.Services
    .AddAuthentication()
    .AddJwtBearer(authenticationProviderKey, options);



builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

await app.UseOcelot();

app.Run();
