using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models;

public class Usuario : IdentityUser
{
    public string? CPF { get; set; }
    public DateTime DataNascimento { get; set; }
    public Usuario() : base()
    {

    }
}
