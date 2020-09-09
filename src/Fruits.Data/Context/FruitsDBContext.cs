using System.Linq;
using Microsoft.EntityFrameworkCore;
using Fruits.Business.Models;
using Microsoft.Extensions.Configuration;

namespace Fruits.Data.Context
{
    public class FruitsDBContext: DbContext
    {

        public FruitsDBContext(DbContextOptions<FruitsDBContext> options): base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Fruit> Fruits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FruitsDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }


    }

}
