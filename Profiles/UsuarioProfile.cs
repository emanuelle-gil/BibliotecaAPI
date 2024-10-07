using AutoMapper;
using BibliotecaAPI.Data.Dtos;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}
