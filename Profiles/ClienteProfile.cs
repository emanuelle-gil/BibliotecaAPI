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
        CreateMap<Cliente, ReadClienteDTO>()
            .ForMember(dest => dest.LivrosEmprestados, opt => opt.MapFrom(src => src.LivrosEmprestados.Select(le => new LivroEmprestadoDTO
            {
                LivroId = le.LivroId,
                Titulo = le.Livro.Titulo,
                DataEmprestimo = le.DataEmprestimo,
                DataDevolucao = le.DataDevolucao
            })));
    }
}
