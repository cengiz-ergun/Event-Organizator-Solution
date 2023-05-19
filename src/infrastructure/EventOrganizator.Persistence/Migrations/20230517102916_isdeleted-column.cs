using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventOrganizator.Persistence.Migrations
{
    public partial class isdeletedcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93620b76-6b30-4e3e-8a7f-0297df5d9551");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9dfa9ed-0e1f-4ab4-b531-8a95eb7670f0");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a85a2e18-dc50-4e2c-b778-586ae91a409e", "84f3a917-1ea3-42ea-95ce-a806a0fa4511", "Administrator", "Administrator" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f9b3252e-9f5a-4c0c-a1fe-f7b0d8f19e71", "eed27a11-d9bf-4a4f-857b-5cfe88eab965", "Member", "Member" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a85a2e18-dc50-4e2c-b778-586ae91a409e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9b3252e-9f5a-4c0c-a1fe-f7b0d8f19e71");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "93620b76-6b30-4e3e-8a7f-0297df5d9551", "fadf5791-7139-4478-87cb-d3f19b91b2cb", "Administrator", "Administrator" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d9dfa9ed-0e1f-4ab4-b531-8a95eb7670f0", "1729ad94-2786-4b6c-a1fa-71d93a6caf01", "Member", "Member" });
        }
    }
}
