using AutoMapper;
using BibliotecaAPI.Data.Dtos;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Profiles;

public class LivroProfile : Profile
{
    public LivroProfile()
    {
        CreateMap<CreateLivroDTO, Livro>();
        CreateMap<UpdateLivroDTO, Livro>();
        CreateMap<Livro, ReadLivroDTO>();
    }
}
