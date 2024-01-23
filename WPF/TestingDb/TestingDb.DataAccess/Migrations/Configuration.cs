using System.Data.Entity.Migrations;
using TestingDb.Model;

namespace TestingDb.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TestingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TestingDbContext context)
        {
            context.Friends.AddOrUpdate(
                f => f.Name,
                new Friend { Name = "Thomas" },
                new Friend { Name = "Julia" }
            );
        }
    }
}