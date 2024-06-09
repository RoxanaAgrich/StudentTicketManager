using Domain.Entities;
using Infrastrucrure.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastrucrure.Configurations
{
    public class ClassConfiguration:IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable(TableName.Class);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength (255).IsRequired();
        }
    }
}
