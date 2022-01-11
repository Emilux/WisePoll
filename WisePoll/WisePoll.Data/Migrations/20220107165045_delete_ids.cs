using Microsoft.EntityFrameworkCore.Migrations;

namespace WisePoll.Data.Migrations
{
    public partial class delete_ids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollFields_Polls_PollsId",
                table: "PollFields");

            migrationBuilder.DropForeignKey(
                name: "FK_Polls_Users_UsersId",
                table: "Polls");

            migrationBuilder.AlterColumn<int>(
                name: "UsersId",
                table: "Polls",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PollsId",
                table: "PollFields",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PollFields_Polls_PollsId",
                table: "PollFields",
                column: "PollsId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_Users_UsersId",
                table: "Polls",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollFields_Polls_PollsId",
                table: "PollFields");

            migrationBuilder.DropForeignKey(
                name: "FK_Polls_Users_UsersId",
                table: "Polls");

            migrationBuilder.AlterColumn<int>(
                name: "UsersId",
                table: "Polls",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PollsId",
                table: "PollFields",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PollFields_Polls_PollsId",
                table: "PollFields",
                column: "PollsId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_Users_UsersId",
                table: "Polls",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
