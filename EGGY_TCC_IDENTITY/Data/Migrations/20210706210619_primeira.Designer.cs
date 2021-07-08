﻿// <auto-generated />
using System;
using EGGY_TCC_IDENTITY.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EGGY_TCC_IDENTITY.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210706210619_primeira")]
    partial class primeira
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EGGY_TCC_IDENTITY.Models.TB_APOIADOR", b =>
                {
                    b.Property<int>("ID_APOIADOR")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("BL_RECEBE_NOVIDADE")
                        .HasColumnType("bit");

                    b.Property<string>("DE_EMAIL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DE_NOME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DT_ALTERACAO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DT_CADASTRO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DT_INATIVACAO")
                        .HasColumnType("datetime2");

                    b.Property<int>("ID_USUARIO")
                        .HasColumnType("int");

                    b.HasKey("ID_APOIADOR");

                    b.ToTable("TB_APOIADOR");
                });

            modelBuilder.Entity("EGGY_TCC_IDENTITY.Models.TB_IMAGEM", b =>
                {
                    b.Property<int>("ID_IMAGEM")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("ARQUIVO")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("DE_TITULO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_ONG")
                        .HasColumnType("int");

                    b.HasKey("ID_IMAGEM");

                    b.ToTable("TB_IMAGEM");
                });

            modelBuilder.Entity("EGGY_TCC_IDENTITY.Models.TB_NIVEL_ACESSO", b =>
                {
                    b.Property<int>("ID_NIVEL")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DE_NIVEL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_NIVEL");

                    b.ToTable("TB_NIVEL_ACESSO");
                });

            modelBuilder.Entity("EGGY_TCC_IDENTITY.Models.TB_NOTICIA", b =>
                {
                    b.Property<int>("ID_NOTICIA")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DE_CONTEUDO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DE_NOME_FANTASIA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DE_TITULO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DT_POSTAGEM")
                        .HasColumnType("datetime2");

                    b.Property<int>("ID_IMAGEM")
                        .HasColumnType("int");

                    b.Property<int>("NU_CURTIDAS")
                        .HasColumnType("int");

                    b.HasKey("ID_NOTICIA");

                    b.ToTable("TB_NOTICIA");
                });

            modelBuilder.Entity("EGGY_TCC_IDENTITY.Models.TB_ONG", b =>
                {
                    b.Property<int>("ID_ONG")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DE_CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DE_EMAIL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DE_LOGIN_USUARIO_ADM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DE_NOME_FANTASIA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DE_RAZAO_SOCIAL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DE_REPRESENTANTE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DE_TELEFONE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DT_ALTERACAO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DT_CADASTRO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DT_INATIVACAO")
                        .HasColumnType("datetime2");

                    b.Property<int>("ID_STATUS")
                        .HasColumnType("int");

                    b.Property<int>("ID_USUARIO_ADM")
                        .HasColumnType("int");

                    b.HasKey("ID_ONG");

                    b.ToTable("TB_ONG");
                });

            modelBuilder.Entity("EGGY_TCC_IDENTITY.Models.TB_ONG_APOIADOR", b =>
                {
                    b.Property<int>("ID_ONG_APOIADOR")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ID_APOIADOR")
                        .HasColumnType("int");

                    b.Property<int>("ID_ONG")
                        .HasColumnType("int");

                    b.HasKey("ID_ONG_APOIADOR");

                    b.ToTable("TB_ONG_APOIADOR");
                });

            modelBuilder.Entity("EGGY_TCC_IDENTITY.Models.TB_STATUS_ONG", b =>
                {
                    b.Property<int>("ID_STATUS")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DE_STATUS")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_STATUS");

                    b.ToTable("TB_STATUS_ONG");
                });

            modelBuilder.Entity("EGGY_TCC_IDENTITY.Models.TB_STATUS_USUARIO", b =>
                {
                    b.Property<int>("ID_STATUS")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DE_STATUS")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_STATUS");

                    b.ToTable("TB_STATUS_USUARIO");
                });

            modelBuilder.Entity("EGGY_TCC_IDENTITY.Models.TB_USUARIO", b =>
                {
                    b.Property<int>("ID_USUARIO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DE_EMAIL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DE_LOGIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DE_NOME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DE_SENHA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DT_ALTERACAO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DT_CADASTRO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DT_INATIVACAO")
                        .HasColumnType("datetime2");

                    b.Property<int>("ID_APOIADOR")
                        .HasColumnType("int");

                    b.Property<int>("ID_NIVEL_ACESSO")
                        .HasColumnType("int");

                    b.Property<int>("ID_STATUS")
                        .HasColumnType("int");

                    b.HasKey("ID_USUARIO");

                    b.ToTable("TB_USUARIO");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
