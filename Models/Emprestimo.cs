using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Models;

public class Emprestimo
{
    [Key]
    [Required]
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public virtual Cliente Cliente { get; set; }

    public int LivroId { get; set; }
    public virtual Livro Livro { get; set; }

    public DateTime DataEmprestimo { get; set; }
    public DateTime? DataDevolucao { get; set; }

}
