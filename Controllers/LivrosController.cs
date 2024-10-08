using AutoMapper;
using BibliotecaAPI.Data;
using BibliotecaAPI.Data.Dtos;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class LivrosController : ControllerBase
{
    private UsuarioDbContext _context;
    private IMapper _mapper;

    public LivrosController(UsuarioDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um novo livro ao sistema.
    /// </summary>
    /// <param name="livroDTO">Dados do livro a ser adicionado.</param>
    /// <returns>Uma resposta com status 201 Created se o livro for adicionado com sucesso.</returns>
    /// <response code="201">Livro adicionado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    public IActionResult AdicionaLivro([FromBody] CreateLivroDTO livroDTO)
    {
        Livro livro = _mapper.Map<Livro>(livroDTO);
        _context.Livros.Add(livro);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaLivro),
            new { id = livro.Id },
            livro);
    }

    /// <summary>
    /// Busca um livro pelo ID.
    /// </summary>
    /// <param name="id">ID do livro a ser buscado.</param>
    /// <returns>Um objeto livro se encontrado.</returns>
    /// <response code="200">Livro encontrado.</response>
    /// <response code="404">Livro não encontrado.</response>
    [HttpGet("{Id}")]
    public IActionResult BuscaLivro(int id)
    {
        var livro = _context.Livros.FirstOrDefault(livro => livro.Id == id);
        if (livro == null) return NotFound();
        var livroDTO = _mapper.Map<ReadLivroDTO>(livro);
        return Ok(livroDTO);
    }

    /// <summary>
    /// Lista todos os livros cadastrados.
    /// </summary>
    /// <returns>Uma lista de livros.</returns>
    /// <response code="200">Lista de livros retornada com sucesso.</response>
    [HttpGet]
    public IEnumerable<ReadLivroDTO> ListarLivros()
    {
        return _mapper.Map<List<ReadLivroDTO>>(_context.Livros);
    }

    /// <summary>
    /// Atualiza os dados de um livro existente.
    /// </summary>
    /// <param name="Id">ID do livro a ser atualizado.</param>
    /// <param name="livroDTO">Dados atualizados do livro.</param>
    /// <returns>Um status 204 No Content se a atualização for bem-sucedida.</returns>
    /// <response code="204">Livro atualizado com sucesso.</response>
    /// <response code="404">Livro não encontrado.</response>
    [HttpPut("{Id}")]
    public IActionResult AtualizarLivro(int Id, [FromBody] UpdateLivroDTO livroDTO)
    {
        var livro = _context.Livros.FirstOrDefault(livro => livro.Id == Id);
        if (livro == null) return NotFound();
        _mapper.Map(livroDTO, livro);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deleta um livro do sistema.
    /// </summary>
    /// <param name="id">ID do livro a ser deletado.</param>
    /// <returns>Um status 204 No Content se a deleção for bem-sucedida.</returns>
    /// <response code="204">Livro deletado com sucesso.</response>
    /// <response code="404">Livro não encontrado.</response>
    [HttpDelete("{Id}")]
    public IActionResult DeletaLivro(int id)
    {
        var livro = _context.Livros.FirstOrDefault(livro => livro.Id == id);
        if (livro == null) return NotFound();
        _context.Remove(livro);
        _context.SaveChanges(); 
        return NoContent();
    }
}
