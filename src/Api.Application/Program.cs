using Api.CrossCutting.DependencyInjection;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyContext>(
    options => options.UseMySql("Server=localhost;Port=3306;Database=Course;Uid=root;Pwd=root",
    ServerVersion.AutoDetect("Server=localhost;Port=3306;Database=Course;Uid=root;Pwd=root")));

//Chamando a injeção de dependência da camada crosscutting
ConfigureService.ConfigureDependencyService(builder.Services);          //serviços
ConfigureRepository.ConfigureDependencyRepository(builder.Services);    //repositório

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
