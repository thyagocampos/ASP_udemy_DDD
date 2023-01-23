using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
    }
    public class DbTeste : IDisposable
    {
        public string databaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";

        public ServiceProvider Serviceprovider { get; private set; }

        public DbTeste()
        {
            var connectionString = $"Server=localhost;Port=3306;Database={databaseName};Uid=root;Pwd=Tico191185";
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(
                o => o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)),
                ServiceLifetime.Transient);

            Serviceprovider = serviceCollection.BuildServiceProvider();

            using (var context = Serviceprovider.GetService<MyContext>())
            {
                if (context != null)
                {
                    context.Database.EnsureCreated();
                }
            }

        }

        public void Dispose()
        {
            using (var context = Serviceprovider.GetService<MyContext>())
            {
                if (context != null)
                {
                    context.Database.EnsureDeleted();
                }
            }
        }
    }
}

