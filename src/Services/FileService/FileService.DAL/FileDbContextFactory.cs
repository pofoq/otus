using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FileService.DAL;
public class FileDbContextFactory : IDesignTimeDbContextFactory<FileDbContext>
{
    public FileDbContext CreateDbContext(string[] args)
    {
        // Загружаем конфигурацию из файла appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Получаем строку подключения
        var connectionString = configuration.GetSection("AppSettings:ConnectionString").Value
                               ?? throw new InvalidOperationException($"{nameof(FileDbContextFactory)}: Строка подключения не найдена.");

        // Настраиваем DbContext для использования PostgreSQL
        var optionsBuilder = new DbContextOptionsBuilder<FileDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new FileDbContext(optionsBuilder.Options);
    }
}
