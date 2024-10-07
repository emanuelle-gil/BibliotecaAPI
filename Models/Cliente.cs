namespace BibliotecaAPI.Models;

public class Cliente
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public List<Livro> LivrosEmprestados { get; set; }
}
