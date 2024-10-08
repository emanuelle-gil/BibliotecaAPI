using BibliotecaAPI.Data;
using BibliotecaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Services;

public class EmprestimoService
{
    private UsuarioDbContext _context;

    public EmprestimoService(UsuarioDbContext context)
    {
        _context = context;
    }

    public string EmprestarLivro(int clienteId, int livroId)
    {
        var cliente = _context.Clientes.Include(c => c.LivrosEmprestados).FirstOrDefault(c => c.Id == clienteId);
        if (cliente == null)
        {
            return "Cliente não encontrado.";
        }

        var livro = _context.Livros.Include(l => l.Emprestimos).FirstOrDefault(l => l.Id == livroId);
        if (livro == null)
        {
            return "Livro não encontrado.";
        }

        if (livro.EstaEmprestado)
        {
            return "O livro já está emprestado.";
        }

        if (cliente.LivrosEmprestados.Count(e => e.DataDevolucao == null) >= 3)
        {
            return "O cliente já tem 3 livros emprestados.";
        }

        var emprestimo = new Emprestimo
        {
            ClienteId = clienteId,
            LivroId = livroId,
            DataEmprestimo = DateTime.Now
        };

        livro.EstaEmprestado = true;

        _context.Emprestimos.Add(emprestimo);
        _context.SaveChanges();

        return "Empréstimo realizado com sucesso.";
    }

    public string DevolverLivro(int clienteId, int livroId)
    {
        var emprestimo = _context.Emprestimos
            .FirstOrDefault(e => e.ClienteId == clienteId && e.LivroId == livroId && e.DataDevolucao == null);

        if (emprestimo == null)
        {
            return "Nenhum empréstimo ativo encontrado para este cliente e livro.";
        }

        emprestimo.DataDevolucao = DateTime.Now;
        var livro = _context.Livros.Find(livroId);
        livro.EstaEmprestado = false;

        _context.SaveChanges();

        return "Livro devolvido com sucesso.";
    }
}


