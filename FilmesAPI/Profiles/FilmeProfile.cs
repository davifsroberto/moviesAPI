using AutoMapper;
using FilmesAPI.Models;

namespace FilmesAPI;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
    }
}
