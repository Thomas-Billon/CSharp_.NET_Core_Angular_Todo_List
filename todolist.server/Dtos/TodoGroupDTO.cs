using System.ComponentModel.DataAnnotations;

namespace TodoList.Server.Dtos
{
    public class TodoGroupDTO
    {
        public class Create
        {
            public class Command
            {
                [Required]
                public required string Title { get; set; }
            }

            public class Response : ResponseBase
            {
                public required int Id { get; set; }
                public required string Title { get; set; }
            }
        }

        public class Update
        {
            public class Command
            {
                public required string Title { get; set; }
            }

            public class Response : ResponseBase
            {
                public required string Title { get; set; }
            }
        }
    }
}
