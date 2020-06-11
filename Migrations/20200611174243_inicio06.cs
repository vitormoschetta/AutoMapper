using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFSemMigrations.Migrations
{
    public partial class inicio06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    Ativo = table.Column<bool>(nullable: false, defaultValue: true),
                    Sexo = table.Column<string>(unicode: false, nullable: false),
                    Altura = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
