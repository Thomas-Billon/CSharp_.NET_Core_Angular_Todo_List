namespace TodoList.Server.Data
{
	public interface IDbInitializerService
	{
		public Task Init(IServiceProvider services);
	}
}