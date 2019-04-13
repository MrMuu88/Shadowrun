using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shadowrun.DataAccess.Migrations
{
    public partial class AddedSkillGrup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "Skills");

            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "Skills",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SkillGroup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillGroup", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_GroupID",
                table: "Skills",
                column: "GroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_SkillGroup_GroupID",
                table: "Skills",
                column: "GroupID",
                principalTable: "SkillGroup",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_SkillGroup_GroupID",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "SkillGroup");

            migrationBuilder.DropIndex(
                name: "IX_Skills_GroupID",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "Skills");

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Skills",
                nullable: true);
        }
    }
}
