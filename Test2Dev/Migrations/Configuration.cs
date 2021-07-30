namespace Test2Dev.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Test2Dev.Models.EmployeeDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Test2Dev.Models.EmployeeDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Employees.AddOrUpdate(x => x.ID, new Models.Employee()
            {
                Name = "Jorge",
                LastName = "Rocha",
                RFC = "ROMJ941030F27",
                BornDate = new DateTime(1994,10,30),
                Status = Enums.EmployeeStatus.NotSet
            });
        }
    }
}
