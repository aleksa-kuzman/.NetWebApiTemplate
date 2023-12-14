using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Net7.WebApi.Template.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("550a0821-5fcf-41f3-8ace-0b698e70a6a9"), null, "RegularUser", "RegularUser" },
                    { new Guid("5a2e0382-92ae-4e1e-89e2-7b0454c45355"), null, "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("531eee9e-ee49-4ff1-a3c8-728af795cb75"), 0, "a5287c63-ea76-4dd3-85b4-fdec3e79c380", "aleksa.kuzman.996@gmail.com", false, false, null, "ALEKSA.KUZMAN.996@GMAIL.COM", null, "AQAAAAIAAYagAAAAENziF5fWYl7apVsUnPFy9FwWMZ9KRyilZnWwKI164YMJFws7yghyAH/FmfJqLpKm4A==", null, false, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "System Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("5a2e0382-92ae-4e1e-89e2-7b0454c45355"), new Guid("531eee9e-ee49-4ff1-a3c8-728af795cb75") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("550a0821-5fcf-41f3-8ace-0b698e70a6a9"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5a2e0382-92ae-4e1e-89e2-7b0454c45355"), new Guid("531eee9e-ee49-4ff1-a3c8-728af795cb75") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5a2e0382-92ae-4e1e-89e2-7b0454c45355"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("531eee9e-ee49-4ff1-a3c8-728af795cb75"));
        }
    }
}
