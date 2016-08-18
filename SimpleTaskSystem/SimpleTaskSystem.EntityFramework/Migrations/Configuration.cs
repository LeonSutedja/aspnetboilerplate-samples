using SimpleTaskSystem.People;

namespace SimpleTaskSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleTaskSystem.EntityFramework.SimpleTaskSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SimpleTaskSystem.EntityFramework.SimpleTaskSystemDbContext context)
        {
            context.People.AddOrUpdate(
                p => p.Name,
                new Person {Name = "Isaac Asimov"},
                new Person {Name = "Thomas More"},
                new Person {Name = "George Orwell"},
                new Person {Name = "Douglas Adams"}
                );

            context.TaskCriticalities.AddOrUpdate(
                p => p.Id,
                new Tasks.TaskCriticality { Id = 1, Value = "Normal" },
                new Tasks.TaskCriticality { Id = 2, Value = "Medium" },
                new Tasks.TaskCriticality { Id = 3, Value = "High" });
        }
    }
}
