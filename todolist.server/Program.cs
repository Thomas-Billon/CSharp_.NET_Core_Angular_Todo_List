using Microsoft.EntityFrameworkCore;
using TodoList.Server.Data;
using TodoList.Server.Data.Startup;
using TodoList.Server.Services;


var builder = WebApplication.CreateBuilder(args);

builder.ConfigureBuilder()
    .ConfigureDbContext()
    .ConfigureServices();

var app = builder.Build();

await app.ConfigureApp()
    .ConfigureDbMigration();

app.Run();


public static class ProgramExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static WebApplicationBuilder ConfigureDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        return builder;
    }
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IDbInitializerService, DbInitializerService>();

        builder.Services.AddScoped<TodoGroupService>();
        builder.Services.AddScoped<TodoItemService>();

        return builder;
    }

    public static WebApplication ConfigureApp(this WebApplication app)
    {
        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    public static async Task<WebApplication> ConfigureDbMigration(this WebApplication app)
    {
        using (IServiceScope serviceScope = app.Services.CreateScope())
        {
            IServiceProvider services = serviceScope.ServiceProvider;

            await services.GetRequiredService<IDbInitializerService>().Init(services);
        }

        return app;
    }
}
