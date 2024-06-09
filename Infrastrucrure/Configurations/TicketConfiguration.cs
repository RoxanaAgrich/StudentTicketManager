using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using Infrastrucrure.Constant;

namespace Infrastructure.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable(TableName.Ticket);
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Student)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.StudentId);

            builder.HasMany(x => x.WishTickets)
                .WithOne(x => x.Ticket)
                .HasForeignKey(x => x.TicketId);
        }
    }
}
