using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using UrnaEletronica.Dominio.Modelos.Usuarios;
using Microsoft.EntityFrameworkCore;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Dominio.Modelos.Coligacoes;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;
using UrnaEletronica.Dominio.Modelos.Eleicoes.Executivos;
using UrnaEletronica.Dominio.Modelos.Eleicoes.Legislativos;
using UrnaEletronica.Dominio.Modelos.Partidos;
using UrnaEletronica.Dominio.Modelos.LogsVotosBatchs;
using UrnaEletronica.Dominio.Modelos.Resultados;
using System.Reflection.Emit;

namespace UrnaEletronica.Persistencia.Contexto
{
    public class UrnaEletronicaContexto : IdentityDbContext<Usuario, Funcao, int, IdentityUserClaim<int>, UsuarioFuncao, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public UrnaEletronicaContexto(DbContextOptions<UrnaEletronicaContexto> options) : base(options) { }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Coligacao> Coligacoes { get; set; }
        public DbSet<ParametroEleicao> ParametrosEleicoes { get; set; }
        public DbSet<LogVotosBatch> LogsVotosBatches{ get; set; }
        public DbSet<LogVotosBatchErros> LogsVotosBatchesErros { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Resultado> Resultados { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UsuarioFuncao>
            (
               usuarioFuncao =>
               {
                   usuarioFuncao.HasKey(uf => new { uf.UserId, uf.RoleId });

                   usuarioFuncao.HasOne(uf =>  uf.Funcao)
                        .WithMany(f => f.UsuariosFuncoes)
                        .HasForeignKey(uf => uf.RoleId)
                        .IsRequired();

                   usuarioFuncao.HasOne(uf => uf.Usuario)
                        .WithMany(f => f.UsuariosFuncoes)
                        .HasForeignKey(uf => uf.UserId)
                        .IsRequired();
               }

            );

            builder.Entity<Partido>()
                .HasOne(p => p.Coligacao)
                .WithMany(c => c.Partidos)
                .HasForeignKey(p => p.ColigacaoId);

            // Configuração do relacionamento entre Candidato e Coligacao
            builder.Entity<Candidato>()
                .HasOne(c => c.Coligacao)
                .WithMany(c => c.Candidatos)
                .HasForeignKey(c => c.ColigacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração do relacionamento entre Candidato e Partido
            builder.Entity<Candidato>()
                .HasOne(c => c.Partido)
                .WithMany(p => p.Candidatos)
                .HasForeignKey(c => c.PartidoId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
