using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Usuarios;
using Microsoft.EntityFrameworkCore;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Dominio.Modelos.Candidatos;
<<<<<<< HEAD
using UrnaEletronica.Dominio.Modelos.Partidos;
=======
using UrnaEletronica.Dominio.Modelos.Coligacoes;
<<<<<<< HEAD
using UrnaEletronica.Dominio.Modelos.LogVotosBatch;
=======
>>>>>>> main
>>>>>>> main

namespace UrnaEletronica.Persistencia.Contexto
{
    public class UrnaEletronicaContexto : IdentityDbContext<Usuario, Funcao, int, IdentityUserClaim<int>, UsuarioFuncao, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public UrnaEletronicaContexto(DbContextOptions<UrnaEletronicaContexto> options) : base(options) { }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
<<<<<<< HEAD
        public DbSet<Partido> Partidos { get; set; }
=======
        public DbSet<Coligacao> Coligacoes { get; set; }
<<<<<<< HEAD
        public DbSet<LogVotosBatch> LogVotosBatch { get; set; }
        public DbSet<LogVotosBatchErros> LogVotosBatchErros { get; set; }
=======
>>>>>>> main
>>>>>>> main

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UsuarioFuncao>(
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
               });
            builder.Entity<Candidato>(
                candidato =>
                {
                    candidato.HasIndex(c => c.CidadeId);

                    candidato.HasIndex(c => c.LegislativoId);

                    candidato.HasIndex(c => c.ExecutivoId);

                    candidato.HasIndex(c => c.PartidoId);
                });
        }

    }
}
