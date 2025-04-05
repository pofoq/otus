using AutoMapper;
using FileService.Api;
using FileService.Api.Mappers;
using FileService.DAL;
using FileService.DAL.Repositories;
using FileService.DAL.Repositories.Implementation;
using FileService.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;
Console.WriteLine($"Среда выполнения: {environment}");

var appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

// Подключение к базе данных
var conn = appSettings?.ConnectionString
           ?? throw new InvalidOperationException("Строка подключения не найдена.");

var services = builder.Services;

// Регистрация DbContext
services.AddDbContext<FileDbContext>(options =>
    options.UseNpgsql(conn)
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine));
Console.WriteLine("Успешное подключение к базе данных.");

// Add services to the container.

services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

services.AddScoped(typeof(IFileService), typeof(LocalFileService));

var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<FileMappingProfile>();
});
mapperConfiguration.AssertConfigurationIsValid();
services.AddSingleton<IMapper>(new Mapper(mapperConfiguration));


services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

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
