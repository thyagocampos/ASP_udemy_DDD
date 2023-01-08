
using Api.Domain.Interfaces.Services.Interfaces;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        //Addtransient muda a cada requisição.
        public static void ConfigureDependencyService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
        }
    }
}