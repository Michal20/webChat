using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webChat.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Message",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Message",
                newName: "ContactId");

            migrationBuilder.AddColumn<bool>(
                name: "sent",
                table: "Message",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sent",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Message",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "Message",
                newName: "ReceiverId");
        }
    }
}
