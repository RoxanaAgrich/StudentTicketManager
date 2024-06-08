using Domain.Entities;
using Infrastrucrure.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastrucrure.Configurations
{
    public class StudentConfiguration :IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(TableName.Student);
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.LastName).HasMaxLength(225).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(225).IsRequired();
            builder.Property(x => x.Gender).HasMaxLength(50);

            builder.HasOne(x => x.Class)
                    .WithMany(x => x.Students)
                    .HasForeignKey(x => x.ClassId);
        }

    }
}
