
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

            serviceCollection.AddDbContext<MyContext>(
            options => options.UseMySql("Server=localhost;Port=3306;Database=Course;Uid=root;Pwd=Tico191185",
            ServerVersion.AutoDetect("Server=localhost;Port=3306;Database=Course;Uid=root;Pwd=Tico191185")));
        }

    }
}