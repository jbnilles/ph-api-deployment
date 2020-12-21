using Microsoft.EntityFrameworkCore.Migrations;

namespace ph_UserEnv.Migrations
{
    public partial class testw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "contact1Id",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contact2Id",
                table: "Contacts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_contact1Id",
                table: "Contacts",
                column: "contact1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_contact2Id",
                table: "Contacts",
                column: "contact2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_AspNetUsers_contact1Id",
                table: "Contacts",
                column: "contact1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_AspNetUsers_contact2Id",
                table: "Contacts",
                column: "contact2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_AspNetUsers_contact1Id",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_AspNetUsers_contact2Id",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_contact1Id",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_contact2Id",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "contact1Id",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "contact2Id",
                table: "Contacts");
        }
    }
}
