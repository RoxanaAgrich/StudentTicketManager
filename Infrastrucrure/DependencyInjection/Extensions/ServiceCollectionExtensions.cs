using Application.Abtractions;
using Domain.Abtractions.DbContext;
using Domain.Abtractions.Repositories;
using Domain.Abtractions.UnitOfWork;
using Domain.Entities.Identity;
using Infrastrucrure.Authentication;
using Infrastrucrure.Caching;
using Infrastrucrure.DependencyInjection.Options;
using Infrastrucrure.Interceptors;
using Infrastrucrure.Repositoties;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastrucrure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IJwtTokenService, JwtTokenService>().AddTransient<ICacheService, CacheService>();
    }
    public static void AddRedisService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(redisOptions =>
        {
            var connectionString = configuration.GetConnectionString("Redis");
            redisOptions.Configuration = connectionString;
        });
    }

    public static void AddSqlServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInterceptorDbContext();
        services.ConfigureSqlServerRetryOptions(configuration.GetSection(nameof(SqlServerRetryOptions)));
        services.AddSqlConfiguration();
        services.AddRepositoryBaseConfiguration();
    }

    public static void AddSqlConfiguration(this IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, ApplicationDbContext>((provider, builder) =>
        {
            var auditableInterceptor = provider.GetService<UpdateAuditableEntitiesInterceptor>();

            var configuration = provider.GetRequiredService<IConfiguration>();
            var options = provider.GetRequiredService<IOptionsMonitor<SqlServerRetryOptions>>();

            builder
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true)
            .UseLazyLoadingProxies(true)
            .UseSqlServer(
                connectionString: configuration.GetConnectionString("ConnectionStrings"),
                sqlServerOptionsAction: optionsBuilder
                        => optionsBuilder.ExecutionStrategy(
                                dependencies => new SqlServerRetryingExecutionStrategy(
                                    dependencies: dependencies,
                                    maxRetryCount: options.CurrentValue.MaxRetryCount,
                                    maxRetryDelay: options.CurrentValue.MaxRetryDelay,
                                    errorNumbersToAdd: options.CurrentValue.ErrorNumbersToAdd))
                            .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name))
            .AddInterceptors(auditableInterceptor);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

        services.AddIdentityCore<AppUser>(opt =>
        {
            opt.Lockout.AllowedForNewUsers = true;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            opt.Lockout.MaxFailedAccessAttempts = 3;
        })
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
            options.Lockout.AllowedForNewUsers = true;
        });
    }

    public static void AddInterceptorDbContext(this IServiceCollection services)
    {
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
    }

    public static void AddRepositoryBaseConfiguration(this IServiceCollection services)
    {
        services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
        services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
    }

    public static OptionsBuilder<SqlServerRetryOptions> ConfigureSqlServerRetryOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<SqlServerRetryOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}
