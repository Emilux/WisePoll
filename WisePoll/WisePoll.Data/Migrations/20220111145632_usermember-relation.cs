using Microsoft.EntityFrameworkCore.Migrations;

namespace WisePoll.Data.Migrations
{
    public partial class usermemberrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Users_UsersId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_UsersId",
                table: "Members");

            migrationBuilder.CreateTable(
                name: "MembersUsers",
                columns: table => new
                {
                    MembersId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembersUsers", x => new { x.MembersId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_MembersUsers_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembersUsers_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MembersUsers_UsersId",
                table: "MembersUsers",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembersUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Members_UsersId",
                table: "Members",
                column: "UsersId");

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
