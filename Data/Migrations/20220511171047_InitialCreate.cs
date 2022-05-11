using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace final.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "veiculosParaVenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeMarca = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    NomeModelo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Ano = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Cilindrada = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Preco = table.Column<double>(type: "float", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veiculosParaVenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_veiculosParaVenda_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Licitacoes",
                columns: table => new
                {
                    LicitacoesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    licitador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    valorLicitado = table.Column<double>(type: "float", nullable: false),
                    veiculosParaVendaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licitacoes", x => x.LicitacoesId);
                    table.ForeignKey(
                        name: "FK_Licitacoes_veiculosParaVenda_veiculosParaVendaId",
                        column: x => x.veiculosParaVendaId,
                        principalTable: "veiculosParaVenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Licitacoes_veiculosParaVendaId",
                table: "Licitacoes",
                column: "veiculosParaVendaId");

            migrationBuilder.CreateIndex(
                name: "IX_veiculosParaVenda_CategoriaId",
                table: "veiculosParaVenda",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Licitacoes");

            migrationBuilder.DropTable(
                name: "veiculosParaVenda");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
