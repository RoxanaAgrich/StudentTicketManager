using Domain.Entities.Identity;
using Domain.Entities;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Domain.Abtractions.DbContext
{
    public interface IApplicationDbContext
    {
        public DbSet<AppUser> AppUses { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<ActionInFunction> ActionInFunctions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Wish> Wishes { get; set; }
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
        DatabaseFacade  Database { get; }
    }
}
