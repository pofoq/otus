using AutoMapper;
using FileService.DAL.Entities;
using FileService.Domain.Models;

namespace FileService.Api.Mappers;

public class FileMappingProfile : Profile
{
    public FileMappingProfile()
    {
        CreateMap<FileModel, FileEntity>();
    }
}
