using Domain.Entities;
using Infrastrucrure.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastrucrure.Configurations
{
    public class WishConfiguration: IEntityTypeConfiguration<Wish>
    {
        public void Configure(EntityTypeBuilder<Wish> builder)
        {
            builder.ToTable(TableName.Wish);
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Name).HasMaxLength(225).IsRequired();
            builder.HasMany(x => x.Tickets)
                .WithMany(x => x.Wishes);

        }
    }
}
