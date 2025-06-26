namespace TodoList.Server.Dtos
{
    public abstract class ResponseBase
    {
        public required bool IsSuccess { get; set; }
    }
}
