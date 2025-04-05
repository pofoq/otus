namespace FileService.Domain.Models;
public class FileModelWithData : FileModel
{
  public byte[] Data { get; set; } = null!;
}
