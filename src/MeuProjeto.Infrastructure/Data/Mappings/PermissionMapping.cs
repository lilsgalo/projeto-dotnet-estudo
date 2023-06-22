using MeuProjeto.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuProjeto.Infrastructure.Data.Mappings
{
    public class PermissionMapping : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasIndex(p => new { p.Type, p.Value }).IsUnique();

            builder.Property(p => p.Type)
                .IsRequired();

            builder.Property(p => p.Value)
                .IsRequired();
        }
    }
}