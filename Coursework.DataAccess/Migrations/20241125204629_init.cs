using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coursework.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pech",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tPech = table.Column<float>(type: "real", nullable: false),
                    height = table.Column<float>(type: "real", nullable: false),
                    tNach = table.Column<float>(type: "real", nullable: false),
                    kTeplo = table.Column<float>(type: "real", nullable: false),
                    Teplo = table.Column<float>(type: "real", nullable: false),
                    p = table.Column<float>(type: "real", nullable: false),
                    tPov = table.Column<float>(type: "real", nullable: false),
                    kTeploOtd = table.Column<float>(type: "real", nullable: false),
                    VremNagr = table.Column<float>(type: "real", nullable: false),
                    T = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pech", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pech");
        }
    }
}
