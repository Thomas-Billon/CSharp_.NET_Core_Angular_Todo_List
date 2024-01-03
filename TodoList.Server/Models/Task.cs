namespace TodoList.Server.Models
{
	public class Task
	{
		public int Id { get; set; }
		public required string Label { get; set; }
		public bool IsCompleted { get; set; }
	}
}
