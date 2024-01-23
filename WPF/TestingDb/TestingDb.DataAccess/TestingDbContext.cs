using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TestingDb.Model;

namespace TestingDb.DataAccess
{
    public class TestingDbContext : DbContext
    {
        public TestingDbContext() : base("TestingDb")
        {

        }

        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
