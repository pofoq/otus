namespace FileService.Api.Dtos;

public sealed class FileDataDto : FileDto
{
  public byte[] Data { get; set; } = null!;
}
