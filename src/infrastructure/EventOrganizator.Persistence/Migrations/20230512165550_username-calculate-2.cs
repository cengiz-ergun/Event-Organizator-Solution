using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventOrganizator.Persistence.Migrations
{
    public partial class usernamecalculate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldComputedColumnSql: "[Email] + ', ' + [FirstName]");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d586f780-61b7-471c-832d-0ea306e92888", "07d4b449-6e22-4564-9021-34d8cb4f25b1", "Administrator", "Administrator" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f77a85ed-b48c-4235-9bf0-6abd5606f547", "5161ddd5-fa5c-4d7a-8ea0-bd0b8b23f867", "Member", "Member" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d586f780-61b7-471c-832d-0ea306e92888");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f77a85ed-b48c-4235-9bf0-6abd5606f547");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                computedColumnSql: "[Email] + ', ' + [FirstName]",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83ad1e28-7e3a-4457-9b24-5fa7d8857695", "5a99da0a-7a47-4d9d-a311-308d4476460c", "Administrator", "Administrator" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b33e85f8-4f21-4cea-9875-a96d6337158a", "c65c182b-2cb1-469c-99d0-5414fbc71668", "Member", "Member" });
        }
    }
}
