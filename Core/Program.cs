using Lift.Buddy.Core.Database;
using Microsoft.EntityFrameworkCore;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<LiftBuddyContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Database") ?? "");
    });

    var app = builder.Build();

    var context = app.Services.GetRequiredService<LiftBuddyContext>();

    context.Database.EnsureCreated();
    context.Database.Migrate();

    app.Run();
}
catch (Exception)
{
    throw;
}

