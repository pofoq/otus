namespace FileService.Api;

public sealed class AppSettings
{
  public string ConnectionString { get; set; } = null!;
  public string FolderPath { get; set; } = null!;
}
