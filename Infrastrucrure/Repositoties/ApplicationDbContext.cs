using Domain.Abtractions.DbContext;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parts.Domain.Entities.Identity;
namespace Infrastrucrure.Repositoties
{
    public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base( )
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) =>
            builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

        public DbSet<AppUser> AppUses { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<ActionInFunction> ActionInFunctions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Class> Classes { get; set; }   
        public DbSet<Student>  Students { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Wish> Wishes { get; set; }
    }
}
