using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class CriarBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: true),
                    Documento = table.Column<string>(type: "varchar(max)", nullable: true),
                    TipoFornecedor = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValue: new DateTime(2020, 1, 16, 15, 27, 56, 214, DateTimeKind.Local).AddTicks(4308))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: true),
                    FornecedorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalSchema: "dbo",
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal", nullable: false),
                    ImagemUpload = table.Column<string>(nullable: true),
                    Imagem = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "DateTime", nullable: false),
                    FornecedorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalSchema: "dbo",
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_FornecedorId",
                schema: "dbo",
                table: "Endereco",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_FornecedorId",
                schema: "dbo",
                table: "Produto",
                column: "FornecedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Produto",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Fornecedor",
                schema: "dbo");
        }
    }
}
