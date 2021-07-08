using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EGGY_TCC_IDENTITY.Data.Migrations
{
    public partial class primeira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_APOIADOR",
                columns: table => new
                {
                    ID_APOIADOR = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DE_NOME = table.Column<string>(nullable: true),
                    DE_EMAIL = table.Column<string>(nullable: true),
                    BL_RECEBE_NOVIDADE = table.Column<bool>(nullable: false),
                    ID_USUARIO = table.Column<int>(nullable: false),
                    DT_CADASTRO = table.Column<DateTime>(nullable: false),
                    DT_ALTERACAO = table.Column<DateTime>(nullable: false),
                    DT_INATIVACAO = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_APOIADOR", x => x.ID_APOIADOR);
                });

            migrationBuilder.CreateTable(
                name: "TB_IMAGEM",
                columns: table => new
                {
                    ID_IMAGEM = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DE_TITULO = table.Column<string>(nullable: true),
                    ARQUIVO = table.Column<byte[]>(nullable: true),
                    ID_ONG = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_IMAGEM", x => x.ID_IMAGEM);
                });

            migrationBuilder.CreateTable(
                name: "TB_NIVEL_ACESSO",
                columns: table => new
                {
                    ID_NIVEL = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DE_NIVEL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_NIVEL_ACESSO", x => x.ID_NIVEL);
                });

            migrationBuilder.CreateTable(
                name: "TB_NOTICIA",
                columns: table => new
                {
                    ID_NOTICIA = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_IMAGEM = table.Column<int>(nullable: false),
                    DE_NOME_FANTASIA = table.Column<string>(nullable: true),
                    DE_TITULO = table.Column<string>(nullable: true),
                    DE_CONTEUDO = table.Column<string>(nullable: true),
                    DT_POSTAGEM = table.Column<DateTime>(nullable: false),
                    NU_CURTIDAS = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_NOTICIA", x => x.ID_NOTICIA);
                });

            migrationBuilder.CreateTable(
                name: "TB_ONG",
                columns: table => new
                {
                    ID_ONG = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USUARIO_ADM = table.Column<int>(nullable: false),
                    DE_LOGIN_USUARIO_ADM = table.Column<string>(nullable: true),
                    DE_REPRESENTANTE = table.Column<string>(nullable: true),
                    DE_EMAIL = table.Column<string>(nullable: true),
                    DE_TELEFONE = table.Column<string>(nullable: true),
                    DE_CNPJ = table.Column<string>(nullable: true),
                    DE_RAZAO_SOCIAL = table.Column<string>(nullable: true),
                    DE_NOME_FANTASIA = table.Column<string>(nullable: true),
                    DT_CADASTRO = table.Column<DateTime>(nullable: false),
                    DT_ALTERACAO = table.Column<DateTime>(nullable: false),
                    DT_INATIVACAO = table.Column<DateTime>(nullable: true),
                    ID_STATUS = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ONG", x => x.ID_ONG);
                });

            migrationBuilder.CreateTable(
                name: "TB_ONG_APOIADOR",
                columns: table => new
                {
                    ID_ONG_APOIADOR = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_ONG = table.Column<int>(nullable: false),
                    ID_APOIADOR = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ONG_APOIADOR", x => x.ID_ONG_APOIADOR);
                });

            migrationBuilder.CreateTable(
                name: "TB_STATUS_ONG",
                columns: table => new
                {
                    ID_STATUS = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DE_STATUS = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STATUS_ONG", x => x.ID_STATUS);
                });

            migrationBuilder.CreateTable(
                name: "TB_STATUS_USUARIO",
                columns: table => new
                {
                    ID_STATUS = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DE_STATUS = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STATUS_USUARIO", x => x.ID_STATUS);
                });

            migrationBuilder.CreateTable(
                name: "TB_USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DE_SENHA = table.Column<string>(nullable: true),
                    DE_LOGIN = table.Column<string>(nullable: true),
                    DE_NOME = table.Column<string>(nullable: true),
                    DE_EMAIL = table.Column<string>(nullable: true),
                    DT_CADASTRO = table.Column<DateTime>(nullable: false),
                    DT_ALTERACAO = table.Column<DateTime>(nullable: false),
                    DT_INATIVACAO = table.Column<DateTime>(nullable: true),
                    ID_APOIADOR = table.Column<int>(nullable: false),
                    ID_STATUS = table.Column<int>(nullable: false),
                    ID_NIVEL_ACESSO = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO", x => x.ID_USUARIO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_APOIADOR");

            migrationBuilder.DropTable(
                name: "TB_IMAGEM");

            migrationBuilder.DropTable(
                name: "TB_NIVEL_ACESSO");

            migrationBuilder.DropTable(
                name: "TB_NOTICIA");

            migrationBuilder.DropTable(
                name: "TB_ONG");

            migrationBuilder.DropTable(
                name: "TB_ONG_APOIADOR");

            migrationBuilder.DropTable(
                name: "TB_STATUS_ONG");

            migrationBuilder.DropTable(
                name: "TB_STATUS_USUARIO");

            migrationBuilder.DropTable(
                name: "TB_USUARIO");
        }
    }
}
