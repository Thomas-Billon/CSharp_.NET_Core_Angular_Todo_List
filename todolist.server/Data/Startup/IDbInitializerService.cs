namespace TodoList.Server.Data.Startup
{
	public interface IDbInitializerService
	{
		public Task Init(IServiceProvider services);
	}
}