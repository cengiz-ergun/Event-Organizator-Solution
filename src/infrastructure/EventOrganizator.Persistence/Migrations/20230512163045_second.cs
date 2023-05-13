using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventOrganizator.Persistence.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01cafe59-f9bd-4b46-9399-ac2b9bf8ec02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "940d5852-4b01-4f74-903d-e2b1166b0eda");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8047040a-7851-41f8-9ebd-e1bc3be5cc90", "eeb211be-590e-4749-9a3a-5e162ea9ff47", "Member", "Member" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "94d24087-7c8a-4ee1-ad68-91e2a3f51b0f", "30b0fa46-1748-4d20-a5a9-3da3d4331824", "Administrator", "Administrator" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8047040a-7851-41f8-9ebd-e1bc3be5cc90");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94d24087-7c8a-4ee1-ad68-91e2a3f51b0f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01cafe59-f9bd-4b46-9399-ac2b9bf8ec02", "902c7f14-8d9b-46ed-8f87-5eb1a19ded2e", "Member", "Member" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "940d5852-4b01-4f74-903d-e2b1166b0eda", "662f9aba-e0e5-4333-8566-6131df40d946", "Administrator", "Administrator" });
        }
    }
}
