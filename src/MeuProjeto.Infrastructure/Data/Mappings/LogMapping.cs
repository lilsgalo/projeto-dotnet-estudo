using MeuProjeto.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuProjeto.Infrastructure.Data.Mappings
{
    public class LogMapping : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder) 
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.TableName);

            builder.Property(p => p.Logger)
                .HasColumnType("ntext");

            builder.Property(p => p.Header)
                .HasColumnType("ntext");

            builder.Property(p => p.Exception)
                .HasColumnType("ntext");

            builder.Property(p => p.StackTracking)
                .HasColumnType("ntext");

            builder.Property(p => p.Message)
                .HasColumnType("ntext");

            builder.Property(p => p.OldState)
                .HasColumnType("ntext");

            builder.Property(p => p.NewState)
                .HasColumnType("ntext");
        }
    }
}
