using Api.CrossCutting.DependencyInjection;
using Api.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(s =>
        {
            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Entre com o token JWT ",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            s.AddSecurityRequirement(new OpenApiSecurityRequirement{
            {
                new OpenApiSecurityScheme{
                    Reference = new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                        }
                },new List<string>()
            }
        });

        });

        var signinConfiguration = new SigninConfiguration();
        builder.Services.AddSingleton(signinConfiguration);

        var tokenConfigurations = new TokenConfiguration();
        new ConfigureFromConfigurationOptions<TokenConfiguration>(
            builder.Configuration.GetSection("TokenConfigurations"))
            .Configure(tokenConfigurations);

        builder.Services.AddSingleton(tokenConfigurations);

        //Autenticação
        builder.Services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOptions =>
        {
            var paramsValidation = bearerOptions.TokenValidationParameters;
            paramsValidation.IssuerSigningKey = signinConfiguration.Key;
            paramsValidation.ValidAudience = tokenConfigurations.Audience;
            paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
            paramsValidation.ValidateIssuerSigningKey = true;
            paramsValidation.ValidateLifetime = true;
            paramsValidation.ClockSkew = TimeSpan.Zero;
        });

        builder.Services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser().Build());
        });

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