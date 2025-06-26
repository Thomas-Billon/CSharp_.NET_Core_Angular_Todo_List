namespace TodoList.Server.Services
{
	public interface IDbInitializerService
	{
		public Task Init(IServiceProvider services);
	}
}