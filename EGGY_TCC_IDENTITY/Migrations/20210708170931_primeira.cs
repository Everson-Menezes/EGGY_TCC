using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EGGY_TCC_IDENTITY.Migrations
{
    public partial class primeira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

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

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
