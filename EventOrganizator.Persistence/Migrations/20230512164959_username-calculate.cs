using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventOrganizator.Persistence.Migrations
{
    public partial class usernamecalculate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8047040a-7851-41f8-9ebd-e1bc3be5cc90");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94d24087-7c8a-4ee1-ad68-91e2a3f51b0f");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                computedColumnSql: "[Email] + ', ' + [FirstName]",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83ad1e28-7e3a-4457-9b24-5fa7d8857695", "5a99da0a-7a47-4d9d-a311-308d4476460c", "Administrator", "Administrator" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b33e85f8-4f21-4cea-9875-a96d6337158a", "c65c182b-2cb1-469c-99d0-5414fbc71668", "Member", "Member" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83ad1e28-7e3a-4457-9b24-5fa7d8857695");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b33e85f8-4f21-4cea-9875-a96d6337158a");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComputedColumnSql: "[Email] + ', ' + [FirstName]");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8047040a-7851-41f8-9ebd-e1bc3be5cc90", "eeb211be-590e-4749-9a3a-5e162ea9ff47", "Member", "Member" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "94d24087-7c8a-4ee1-ad68-91e2a3f51b0f", "30b0fa46-1748-4d20-a5a9-3da3d4331824", "Administrator", "Administrator" });
        }
    }
}
