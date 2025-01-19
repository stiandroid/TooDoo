using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TooDoo.Migrations
{
    /// <inheritdoc />
    public partial class TaskLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "TaskItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskListId",
                table: "TaskItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaskLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_TaskListId",
                table: "TaskItems",
                column: "TaskListId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_TaskLists_TaskListId",
                table: "TaskItems",
                column: "TaskListId",
                principalTable: "TaskLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_TaskLists_TaskListId",
                table: "TaskItems");

            migrationBuilder.DropTable(
                name: "TaskLists");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_TaskListId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "TaskListId",
                table: "TaskItems");
        }
    }
}
