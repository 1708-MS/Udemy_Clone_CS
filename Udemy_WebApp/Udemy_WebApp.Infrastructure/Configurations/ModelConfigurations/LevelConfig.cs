using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy_WebApp.Domain.Models;

namespace Udemy_WebApp.Infrastructure.Configurations.ModelConfigurations
{
    public class LevelConfig : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.HasKey(Level => Level.LevelId);
            builder.Property(Level => Level.LevelName).IsRequired();
        }
    }
}
