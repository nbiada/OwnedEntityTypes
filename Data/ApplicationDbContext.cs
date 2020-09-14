using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OwnedEntityTypes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        /// <summary>
        /// Model builder
        /// Owned Entity Types from https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Person>(p =>
            {
                p.HasKey(p => p.Id);
                p.OwnsOne(p => p.MainAddress);
                p.OwnsOne(p => p.SecondaryAddress);
                p.OwnsOne(p => p.Phone);
            });
        }
    }
}
