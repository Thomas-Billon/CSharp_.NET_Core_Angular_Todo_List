using TodoList.Server.Dtos;
using TodoList.Server.Entities;
using TodoList.Server.Queries;

namespace TodoList.Server.Mappers
{
    public class TodoItemMapper
    {
        public TodoItemDTO.GetAll.Response ToGetAllResponse(IReadOnlyList<TodoItemQuery> queries)
        {
            return new TodoItemDTO.GetAll.Response
            {
                Items = queries.Select(x => ToGetResponse(x)).ToList();
            };
        }

        public TodoItemDTO.Get.Response ToGetResponse(TodoItemQuery query)
        {
            return new TodoItemDTO.Get.Response
            {
                Id = query.Id,
                Title = query.Title,
                IsCompleted = query.IsCompleted
            };
        }

        public TodoItem ToEntity(TodoItemDTO.Create.Command command)
        {
            return new TodoItem
            {
                Title = command.Title
            };
        }

        public TodoItemDTO.Create.Response ToCreateResponse(TodoItem entity)
        {
            return new TodoItemDTO.Create.Response
            {
                Id = entity.Id,
                Title = entity.Title,
                IsCompleted = entity.IsCompleted
            };
        }

        public TodoItem ToEntity(TodoItemDTO.Update.Command command)
        {
            return new TodoItem
            {
                Title = command.Title,
                IsCompleted = command.IsCompleted
            };
        }

        public TodoItemDTO.Update.Response ToUpdateResponse(TodoItem entity)
        {
            return new TodoItemDTO.Update.Response
            {
                Title = entity.Title,
                IsCompleted = entity.IsCompleted
            };
        }

        public string ToTitle(TodoItemDTO.UpdateTitle.Command command)
        {
            return command.Title;
        }

        public TodoItemDTO.UpdateTitle.Response ToUpdateTitleResponse(string title)
        {
            return new TodoItemDTO.UpdateTitle.Response
            {
                Title = title
            };
        }

        public bool ToIsCompleted(TodoItemDTO.UpdateIsCompleted.Command command)
        {
            return command.IsCompleted;
        }

        public TodoItemDTO.UpdateIsCompleted.Response ToUpdateIsCompletedResponse(bool isCompleted)
        {
            return new TodoItemDTO.UpdateIsCompleted.Response
            {
                IsCompleted = isCompleted
            };
        }
    }
}
