using System.Data.Entity;
using Core.Entities.Concrete;

namespace DataAccess.Context
{
    public class SqlContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<SqlContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Polygon> Polygons { get; set; }
    }
}