using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MeuProjeto.Business.Models;

namespace MeuProjeto.Infrastructure.Data.Mappings
{
    public class SettingsMapping : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Code)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(a => a.Contents)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(a => a.Type)
                .IsRequired();

            builder.ToTable("Settings");
        } 

    }
}
