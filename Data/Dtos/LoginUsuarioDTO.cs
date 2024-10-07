using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Data.Dtos;

public class LoginUsuarioDTO
{
    [Required]
    public string CPF { get; set; }
    [Required]
    public string Senha { get; set; }
}
