namespace TodoList.Server.Services.Startup
{
	public interface IDbInitializerService
	{
		public Task Init(IServiceProvider services);
	}
}