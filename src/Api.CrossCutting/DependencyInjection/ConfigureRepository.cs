
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        //Addscoped dura a cada requisição. Sempre que solicitado, será retornado o mesmo objeto
        public static void ConfigureDependencyRepository(IServiceCollection serviceCollection)
        {
            //Tem que ser typeof porque na injeção de dependência utilizarmos um generic
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            serviceCollection.AddScoped<IUserRepository, UserImplementation>();
            
            string? env = "";
            string? dbcon = "";

            dbcon = Environment.GetEnvironmentVariable("DB_CONNECTION");

            if (Environment.GetEnvironmentVariable("DATABASE") != null)
                env = Environment.GetEnvironmentVariable("DATABASE");

            if (env != null)
            {
                if (env.ToLower() == "SQL".ToLower())
                {
                    if (dbcon != null)
                    {
                        serviceCollection.AddDbContext<MyContext>(options =>
                                        options.UseSqlServer(dbcon));
                    }
                }
                else if (env.ToLower() == "MYSQL".ToLower())
                {
                    serviceCollection.AddDbContext<MyContext>(
                    options => options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION"),
                    ServerVersion.AutoDetect(dbcon)));
                }
            }
        }
    }
}