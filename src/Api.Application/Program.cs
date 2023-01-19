using Api.CrossCutting.DependencyInjection;
using Api.Domain.Security;
using Microsoft.Extensions.Options;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var signinConfiguration = new SigninConfiguration();
        builder.Services.AddSingleton(signinConfiguration);

        var tokenConfigurations = new TokenConfiguration();
        new ConfigureFromConfigurationOptions<TokenConfiguration>(
            builder.Configuration.GetSection("TokenConfigurations"))
            .Configure(tokenConfigurations);

        builder.Services.AddSingleton(tokenConfigurations);

        //Chamando a injeção de dependência da camada crosscutting
        ConfigureService.ConfigureDependencyService(builder.Services);          //serviços
        ConfigureRepository.ConfigureDependencyRepository(builder.Services);    //repositório

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}