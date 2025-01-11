using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using TodoList.Server.Data;
using TodoList.Server.Entities;
using TodoList.Server.Services;

namespace todolist.server.test.Services
{
    [TestClass]
    public class TodoItemServiceTest
    {
        private readonly Mock<ApplicationDbContext> _dbContext;
        private readonly ITodoItemService _todoItemService;

        public TodoItemServiceTest()
        {
            var options = new DbContextOptionsBuilder().Options;
            var entities = TodoItemData.GenerateList().AsQueryable();

            _dbContext = new Mock<ApplicationDbContext>(options);
            _todoItemService = new TodoItemService(_dbContext.Object);

            _dbContext
                .Setup(ctx => ctx.TodoItems)
                .ReturnsDbSet(entities);
        }

        [TestInitialize]
        public void Init()
        {
            // Method runs before each test
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Method runs after each test
        }

        [TestMethod]
        public async Task GetAll_Should_Return_TodoItemList()
        {
            // Arrange
            // N/A

            // Act
            var result = await _todoItemService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 3);
        }

        [TestMethod]
        public async Task GetById_Should_Return_TodoItem()
        {
            // Arrange
            var id = 3;

            // Act
            var result = await _todoItemService.GetById(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id == id);
        }

        [TestMethod]
        public async Task GetById_Should_Return_Null()
        {
            // Arrange
            var id = 4;

            // Act
            var result = await _todoItemService.GetById(id);

            // Assert
            Assert.IsNull(result);
        }

        // TODO: Add more useful tests later

        #region DATA

        public static class TodoItemData
        {
            public static List<TodoItem> GenerateList() => new List<TodoItem>
            {
                new()
                {
                    Id = 1,
                    Label = "Hello",
                    IsCompleted = false
                },
                new()
                {
                    Id = 2,
                    Label = "world",
                    IsCompleted = false
                },
                new()
                {
                    Id = 3,
                    Label = "!",
                    IsCompleted = true
                }
            };
        }

        #endregion DATA
    }
}