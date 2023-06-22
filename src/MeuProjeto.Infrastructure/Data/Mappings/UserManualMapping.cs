using MeuProjeto.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MeuProjeto.Infrastructure.Data.Mappings
{
    public class UserManualMapping : IEntityTypeConfiguration<UserManual>
    {
        public void Configure(EntityTypeBuilder<UserManual> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Code)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(a => a.Revision)
                .IsRequired();

            builder.ToTable("UserManuals");
        }

    }
}
