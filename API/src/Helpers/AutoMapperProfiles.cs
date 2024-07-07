using AutoMapper;
using src.DTOs;
using src.Models;

namespace src.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CoverLetterDto, CoverLetter>();
        CreateMap<CoverLetter, CoverLetterDto>();
    }
}