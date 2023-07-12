using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Application.Features.Trainings.Repositories;
using University.Application.UnitOfWorks;
using University.Domain.Entities;
using University.Domain.UnitOfWork;
using University.Persistence.Features.Membership;

namespace University.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
           ApplicationRole, Guid,
           ApplicationUserClaim, ApplicationUserRole,
           ApplicationUserLogin, ApplicationRoleClaim,
           ApplicationUserToken>,
           IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public ApplicationDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    (x) => x.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> Students { get; set; }

    }
}
