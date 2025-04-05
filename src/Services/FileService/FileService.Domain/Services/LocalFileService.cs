using AutoMapper;
using FileService.DAL.Entities;
using FileService.DAL.Repositories;
using FileService.Domain.Models;

namespace FileService.Domain.Services;

public class LocalFileService(
  IRepository<FileEntity> repository,
  IMapper mapper
  )
  : IFileService
{
  private readonly IRepository<FileEntity> _repository = repository;
  private readonly IMapper _mapper = mapper;

  public Task DeleteFileAsync(Guid guid)
  {
    return _repository.RemoveAsync(guid);
  }

  public async Task<FileModel?> GetFileAsync(Guid guid)
  {
    var entity = await _repository.GetAsync(guid);
    return _mapper.Map<FileModel?>(entity);
  }

    public Task<byte[]?> GetFileDataAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task SaveFileAsync(string fileName, byte[] fileBytes)
  {
    throw new NotImplementedException();
  }
}
