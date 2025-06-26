using TodoList.Server.Entities;

namespace TodoList.Server.Dtos
{
    public record TodoGroupDTO(
        int Id,
        string Title
    );

    public static class TodoGroupExtension
    {
        public static TodoGroupDTO ToDTO(this TodoGroup x) => new TodoGroupDTO
        (
            x.Id,
            x.Title
        );

        public static TodoGroup ToEntity(this TodoGroupDTO x) => new TodoGroup
        {
            Id = x.Id,
            Title = x.Title
        };
    }
}
