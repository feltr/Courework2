using Coursework.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.DataAccess.Configurations
{
    public class PechConfiguration : IEntityTypeConfiguration<PechEntity>
    {
        public void Configure(EntityTypeBuilder<PechEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.tPech).IsRequired();
            builder.Property(b => b.height).IsRequired();
            builder.Property(b => b.tNach).IsRequired();
            builder.Property(b => b.kTeplo).IsRequired();
            builder.Property(b => b.Teplo).IsRequired();
            builder.Property(b => b.p).IsRequired();
            builder.Property(b => b.tPov).IsRequired();
            builder.Property(b => b.kTeploOtd).IsRequired();
            builder.Property(b => b.VremNagr).IsRequired();
            builder.Property(b => b.T).IsRequired();
        }
    }
}
