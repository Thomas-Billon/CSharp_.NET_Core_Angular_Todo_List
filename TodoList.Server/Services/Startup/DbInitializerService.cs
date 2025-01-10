using Microsoft.EntityFrameworkCore;
using TodoList.Server.Data;

namespace TodoList.Server.Services.Startup
{
	class DbInitializerService : IDbInitializerService
	{
		public async Task Init(IServiceProvider services)
		{
			var logger = services.GetRequiredService<ILogger<Program>>();
			var dbContext = services.GetRequiredService<ApplicationDbContext>();

			try
			{
				await dbContext.Database.MigrateAsync();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred initializing the database context.");
				throw;
			}
		}
	}
}