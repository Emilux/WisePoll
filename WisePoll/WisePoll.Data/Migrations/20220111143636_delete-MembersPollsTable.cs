using Microsoft.EntityFrameworkCore.Migrations;

namespace WisePoll.Data.Migrations
{
    public partial class deleteMembersPollsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Users_UsersId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "MembersPolls");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Members",
                newName: "PollsId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_UsersId",
                table: "Members",
                newName: "IX_Members_PollsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Polls_PollsId",
                table: "Members",
                column: "PollsId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Polls_PollsId",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "PollsId",
                table: "Members",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_PollsId",
                table: "Members",
                newName: "IX_Members_UsersId");

            migrationBuilder.CreateTable(
                name: "MembersPolls",
                columns: table => new
                {
                    MembersId = table.Column<int>(type: "int", nullable: false),
                    PollsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembersPolls", x => new { x.MembersId, x.PollsId });
                    table.ForeignKey(
                        name: "FK_MembersPolls_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembersPolls_Polls_PollsId",
                        column: x => x.PollsId,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MembersPolls_PollsId",
                table: "MembersPolls",
                column: "PollsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Users_UsersId",
                table: "Members",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
