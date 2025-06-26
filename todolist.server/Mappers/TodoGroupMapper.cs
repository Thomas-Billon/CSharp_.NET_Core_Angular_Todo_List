using TodoList.Server.Dtos;
using TodoList.Server.Entities;
using TodoList.Server.Queries;

namespace TodoList.Server.Mappers
{
    public class TodoGroupMapper
    {
        public TodoGroupDTO.GetAll.Response ToGetAllResponse(IReadOnlyList<TodoGroupQuery> queries)
        {
            return new TodoGroupDTO.GetAll.Response
            {
                Groups = queries.Select(ToGetResponse).ToList()
            };
        }

        public TodoGroupDTO.Get.Response ToGetResponse(TodoGroupQuery query)
        {
            return new TodoGroupDTO.Get.Response
            {
                Id = query.Id,
                Title = query.Title
            };
        }

        public TodoGroup ToEntity(TodoGroupDTO.Create.Command command)
        {
            return new TodoGroup
            {
                Title = command.Title
            };
        }

        public TodoGroupDTO.Create.Response ToCreateResponse(TodoGroup entity)
        {
            return new TodoGroupDTO.Create.Response
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }

        public TodoGroup ToEntity(TodoGroupDTO.Update.Command command)
        {
            return new TodoGroup
            {
                Title = command.Title
            };
        }

        public TodoGroupDTO.Update.Response ToUpdateResponse(TodoGroup entity)
        {
            return new TodoGroupDTO.Update.Response
            {
                Title = entity.Title
            };
        }

        public string ToTitle(TodoGroupDTO.UpdateTitle.Command command)
        {
            return command.Title;
        }

        public TodoGroupDTO.UpdateTitle.Response ToUpdateTitleResponse(string title)
        {
            return new TodoGroupDTO.UpdateTitle.Response
            {
                Title = title
            };
        }
    }
}
