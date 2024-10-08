namespace BibliotecaAPI.Data.Dtos;

public class LivroEmprestadoDTO
{
    public int LivroId { get; set; }
    public string Titulo { get; set; }
    public DateTime DataEmprestimo { get; set; }
    public DateTime? DataDevolucao { get; set; }
}
