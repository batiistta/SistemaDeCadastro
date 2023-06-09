using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Ignore(x => x.ConfirmeEmail);
            builder.Property(x => x.Senha).IsRequired();
            builder.Ignore(x => x.ConfirmeSenha);            
            builder.Property(x => x.Perfil).IsRequired();
            builder.Property(x => x.DataCadastro).IsRequired();

            builder.HasMany(x => x.Contatos);
        }
    }
}
