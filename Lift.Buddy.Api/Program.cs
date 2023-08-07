using Lift.Buddy.API.Interfaces;
using Lift.Buddy.API.Services;
using Microsoft.Data.Sqlite;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5200");

var anyCors = "anyCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: anyCors,
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(anyCors);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();

app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.Run();
