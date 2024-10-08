using BibliotecaAPI.Models;

namespace BibliotecaAPI.Data.Dtos;

public class ReadClienteDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public ICollection<LivroEmprestadoDTO> LivrosEmprestados { get; set; }
}
