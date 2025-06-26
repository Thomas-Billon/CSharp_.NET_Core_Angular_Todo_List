using Microsoft.EntityFrameworkCore;
using TodoList.Server.Data;

namespace TodoList.Server.Services
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
				logger.LogError(ex, "Error: Database context could not be initialized");
				throw;
			}
		}
	}
}