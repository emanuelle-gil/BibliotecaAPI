using AutoMapper;
using BibliotecaAPI.Data.Dtos;
using BibliotecaAPI.Models;

namespace BibliotecaAPI.Profiles;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<CreateClienteDTO, Cliente>();
        CreateMap<UpdateClienteDTO, Cliente>();
        CreateMap<Cliente, ReadClienteDTO>();
    }
}
