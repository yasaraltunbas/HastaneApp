using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace HastaneAPP.Migrations
{
    public partial class HastaneDbModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hastalars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TCNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Applicant = table.Column<string>(nullable: true),
                    Operation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hastalars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operasyonlars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operasyonlars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "extraHastaliks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Question = table.Column<string>(nullable: true),
                    Answers = table.Column<bool>(nullable: false),
                    HastalarId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_extraHastaliks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_extraHastaliks_Hastalars_HastalarId",
                        column: x => x.HastalarId,
                        principalTable: "Hastalars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_extraHastaliks_HastalarId",
                table: "extraHastaliks",
                column: "HastalarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "extraHastaliks");

            migrationBuilder.DropTable(
                name: "Operasyonlars");

            migrationBuilder.DropTable(
                name: "Hastalars");
        }
    }
}
