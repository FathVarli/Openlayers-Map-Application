using System.Data.Entity;
using Core.Entities.Concrete;

namespace DataAccess.DataBase
{
    public class SqlContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<SqlContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}