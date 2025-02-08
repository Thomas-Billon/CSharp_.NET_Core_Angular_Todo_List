using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddTodoGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Label",
                table: "TodoItems",
                newName: "Title");

            migrationBuilder.AddColumn<int>(
                name: "TodoGroupId",
                table: "TodoItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TodoGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_TodoGroupId",
                table: "TodoItems",
                column: "TodoGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_TodoGroups_TodoGroupId",
                table: "TodoItems",
                column: "TodoGroupId",
                principalTable: "TodoGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_TodoGroups_TodoGroupId",
                table: "TodoItems");

            migrationBuilder.DropTable(
                name: "TodoGroups");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_TodoGroupId",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "TodoGroupId",
                table: "TodoItems");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TodoItems",
                newName: "Label");
        }
    }
}
