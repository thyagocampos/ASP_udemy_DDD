using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost;Port=3306;Database=dbapi;Uid=root;Pwd=Tico191185";
            //var connectionString = "server=.\\SQLEXPRESS; Database=dbapi; Trusted_Connection=true;";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            //optionsBuilder.UseSqlServer(connectionString);
            return new MyContext (optionsBuilder.Options);
        }
    }
}