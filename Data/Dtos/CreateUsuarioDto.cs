using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Data.Dtos;

public class CreateUsuarioDto
{
    [Required]
    public string CPF { get; set; }
    [Required]
    public DateTime DataNascimento { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Senha { get; set; }
    [Required]
    [Compare("Senha")]
    public string SenhaConfirmation { get; set; } 
}
