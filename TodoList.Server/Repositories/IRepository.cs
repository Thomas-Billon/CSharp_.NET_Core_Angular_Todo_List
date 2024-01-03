namespace TodoList.Server.Repositories
{
	public interface IRepository<T>
	{
		public IEnumerable<T> GetAll();
		public T Get(int id);
		public int Create(T item);
		public bool Update(T item);
		public bool Delete(int id);
		public bool Save();
	}
}
