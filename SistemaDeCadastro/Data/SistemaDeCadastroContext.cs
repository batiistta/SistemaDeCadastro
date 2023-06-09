using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaDeCadastro.Data.Map;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Data
{
    public class SistemaDeCadastroContext : DbContext
    {
        public SistemaDeCadastroContext(DbContextOptions<SistemaDeCadastroContext> options) : base(options) { }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Contato> contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ContatoMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}