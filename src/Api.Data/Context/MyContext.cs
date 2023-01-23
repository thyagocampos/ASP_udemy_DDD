using Api.Data.Mapping;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity>? Users { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Why use model builder?
            //In Entity Framework Core, the ModelBuilder class acts as a Fluent API. By using it, we can configure many different things, as it provides more configuration options than data annotation attributes. 
            //https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

            modelBuilder.Entity<UserEntity>(new UserMap().Configure);

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrador",
                    Email = "adm@email.com",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                }
            );
        }
    }
}