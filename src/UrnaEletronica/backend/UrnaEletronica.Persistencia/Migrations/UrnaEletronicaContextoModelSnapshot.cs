﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UrnaEletronica.Persistencia.Contexto;

#nullable disable

namespace UrnaEletronica.Persistencia.Migrations
{
    [DbContext(typeof(UrnaEletronicaContexto))]
    partial class UrnaEletronicaContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Candidatos.Candidato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CidadeId")
                        .HasColumnType("int");

                    b.Property<int?>("ColigacaoId")
                        .HasColumnType("int");

                    b.Property<int>("ColigacoaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("EhExecutivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("EhLegislativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FotoURL")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<int>("PartidoId")
                        .HasColumnType("int");

                    b.Property<int>("QtdVotos")
                        .HasColumnType("int");

                    b.Property<string>("TipoCandidatura")
                        .HasColumnType("longtext");

                    b.Property<bool>("VotosValidos")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("CidadeId");

                    b.HasIndex("ColigacaoId");

                    b.HasIndex("PartidoId");

                    b.ToTable("Candidatos");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Cidades.Cidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("NomeEstado")
                        .HasColumnType("longtext");

                    b.Property<int>("QtdHabitantes")
                        .HasColumnType("int");

                    b.Property<string>("SiglaEstado")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cidades");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Coligacoes.Coligacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<int>("QtdVotos")
                        .HasColumnType("int");

                    b.Property<string>("Sigla")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Coligacoes");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.LogsVotosBatchs.LogVotosBatch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CandidatoId")
                        .HasColumnType("int");

                    b.Property<int>("CidadeId")
                        .HasColumnType("int");

                    b.Property<int>("ColigacaoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHoraRecebimento")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PartidoId")
                        .HasColumnType("int");

                    b.Property<int>("QtdVotos")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LogsVotosBatches");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.LogsVotosBatchs.LogVotosBatchErros", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CandidatoId")
                        .HasColumnType("int");

                    b.Property<int>("CidadeId")
                        .HasColumnType("int");

                    b.Property<int>("ColigacaoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHoraRecebimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MensagemErro")
                        .HasColumnType("longtext");

                    b.Property<int>("PartidoId")
                        .HasColumnType("int");

                    b.Property<int>("QtdVotos")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LogsVotosBatchesErros");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.ParametrosEleicoes.ParametroEleicao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CidadeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataEleicaoPrimeiroTurno")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataEleicaoSegundoTurno")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("PrimeiroTurno")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("QtdCadeiras")
                        .HasColumnType("int");

                    b.Property<int>("QtdVotosSomentePrimeiroTurno")
                        .HasColumnType("int");

                    b.Property<bool>("SegundoTurno")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("CidadeId");

                    b.ToTable("ParametrosEleicoes");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Partidos.Partido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ColigacaoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("Sigla")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ColigacaoId");

                    b.ToTable("Partidos");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Resultados.Resultado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("CandidatoEleito")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("CandidatoId")
                        .HasColumnType("int");

                    b.Property<double>("PercentualVotos")
                        .HasColumnType("double");

                    b.Property<int>("QtdVotos")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CandidatoId");

                    b.ToTable("Resultados");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Usuarios.Funcao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NomeFuncao")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Usuarios.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FotoURL")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Usuarios.UsuarioFuncao", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("UrnaEletronica.Dominio.Modelos.Usuarios.Funcao", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("UrnaEletronica.Dominio.Modelos.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("UrnaEletronica.Dominio.Modelos.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("UrnaEletronica.Dominio.Modelos.Usuarios.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Candidatos.Candidato", b =>
                {
                    b.HasOne("UrnaEletronica.Dominio.Modelos.Cidades.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UrnaEletronica.Dominio.Modelos.Coligacoes.Coligacao", "Coligacao")
                        .WithMany("Candidatos")
                        .HasForeignKey("ColigacaoId");

                    b.HasOne("UrnaEletronica.Dominio.Modelos.Partidos.Partido", "Partido")
                        .WithMany("Candidatos")
                        .HasForeignKey("PartidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cidade");

                    b.Navigation("Coligacao");

                    b.Navigation("Partido");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.ParametrosEleicoes.ParametroEleicao", b =>
                {
                    b.HasOne("UrnaEletronica.Dominio.Modelos.Cidades.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cidade");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Partidos.Partido", b =>
                {
                    b.HasOne("UrnaEletronica.Dominio.Modelos.Coligacoes.Coligacao", "Coligacao")
                        .WithMany("Partidos")
                        .HasForeignKey("ColigacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coligacao");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Resultados.Resultado", b =>
                {
                    b.HasOne("UrnaEletronica.Dominio.Modelos.Candidatos.Candidato", "Candidato")
                        .WithMany()
                        .HasForeignKey("CandidatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidato");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Usuarios.UsuarioFuncao", b =>
                {
                    b.HasOne("UrnaEletronica.Dominio.Modelos.Usuarios.Funcao", "Funcao")
                        .WithMany("UsuariosFuncoes")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UrnaEletronica.Dominio.Modelos.Usuarios.Usuario", "Usuario")
                        .WithMany("UsuariosFuncoes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcao");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Coligacoes.Coligacao", b =>
                {
                    b.Navigation("Candidatos");

                    b.Navigation("Partidos");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Partidos.Partido", b =>
                {
                    b.Navigation("Candidatos");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Usuarios.Funcao", b =>
                {
                    b.Navigation("UsuariosFuncoes");
                });

            modelBuilder.Entity("UrnaEletronica.Dominio.Modelos.Usuarios.Usuario", b =>
                {
                    b.Navigation("UsuariosFuncoes");
                });
#pragma warning restore 612, 618
        }
    }
}
