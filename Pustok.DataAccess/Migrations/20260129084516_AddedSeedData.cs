using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pustok.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("ddee9e04-e981-4bef-b9e0-bac96b98a78a"), "Default Category" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ddee9e04-e981-4bef-b9e0-bac96b98a78a"));
        }
    }
}
