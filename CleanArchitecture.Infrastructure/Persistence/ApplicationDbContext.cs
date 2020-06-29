using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
  {
    private readonly ICurrentUserService _currentUserService;
    public ApplicationDbContext(DbContextOptions options, ICurrentUserService currentUserService)
    : base(options)
    {
      _currentUserService = currentUserService;
    }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entry.Entity.CreatedBy
              = _currentUserService.GetCurrentUserName() ?? "admin";
            entry.Entity.Created
              = DateTime.Now;
            break;
          case EntityState.Modified:
            entry.Entity.LastModifiedBy
              = _currentUserService.GetCurrentUserName() ?? "admin";
            entry.Entity.LastModified
              = DateTime.Now;
            break;
        }
      }

      return base.SaveChangesAsync(cancellationToken);
    }
  }
}