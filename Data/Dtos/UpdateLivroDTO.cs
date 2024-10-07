namespace BibliotecaAPI.Data.Dtos;

public class UpdateLivroDTO
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; set; }
    public DateTime DataPublicacao { get; set; }
    public bool EstaEmprestado { get; set; }
}
