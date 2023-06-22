using MeuProjeto.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuProjeto.Infrastructure.Data.Mappings
{
    public class TextMapping : IEntityTypeConfiguration<Text>
    {
        public void Configure(EntityTypeBuilder<Text> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(t => t.Value)
                .IsRequired()
                .HasColumnType("text");

        }
    }
}
