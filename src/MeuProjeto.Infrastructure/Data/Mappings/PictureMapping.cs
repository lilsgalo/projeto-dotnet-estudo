using MeuProjeto.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuProjeto.Infrastructure.Data.Mappings
{
    public class PictureMapping : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Value)
                .HasColumnType("nvarchar(max)");

            builder.ToTable("Pictures");
        }
    }
}