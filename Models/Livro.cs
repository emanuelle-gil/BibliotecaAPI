using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models;

public class Livro
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; set; }
    public DateTime DataPublicacao { get; set; }
    public bool EstaEmprestado { get; set; }
    public virtual ICollection<Emprestimo> Emprestimos { get; set; }
}
