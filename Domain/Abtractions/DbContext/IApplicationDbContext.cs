using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Domain.Abtractions.DbContext
{
    public interface IApplicationDbContext
    {
        DbSet<AppUser> AppUses { get; set; }
        DbSet<Action> Actions { get; set; }
        DbSet<Function> Functions { get; set; }
        DbSet<ActionInFunction> ActionInFunctions { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<Class> Classes { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Ticket> Tickets { get; set; }
        DbSet<Wish> Wishes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DatabaseFacade Database { get; }
    }
}
