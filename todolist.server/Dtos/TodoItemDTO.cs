using System.ComponentModel.DataAnnotations;

namespace TodoList.Server.Dtos
{
    public class TodoItemDTO
    {
        public class Create
        {
            public class Command
            {
                [Required]
                public required string Label { get; set; }
            }

            public class Response : ResponseBase
            {
                public required int Id { get; set; }
                public required string Label { get; set; }
                public required bool IsCompleted { get; set; }
            }
        }

        public class Update
        {
            public class Command
            {
                public required string Label { get; set; }
                public required bool IsCompleted { get; set; }
            }

            public class Response : ResponseBase
            {
                public required string Label { get; set; }
                public required bool IsCompleted { get; set; }
            }
        }
    }
}
