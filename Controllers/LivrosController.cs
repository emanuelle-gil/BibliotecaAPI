using AutoMapper;
using BibliotecaAPI.Data;
using BibliotecaAPI.Data.Dtos;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public IActionResult AdicionaLivro([FromBody] CreateLivroDTO livroDTO)
    {
        Livro livro = _mapper.Map<Livro>(livroDTO);
        _context.Livros.Add(livro);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaLivro),
            new { titulo = livro.Titulo },
            livro);
    }

    [HttpGet("{Id}")]
    public IActionResult BuscaLivro(int id)
    {
        var livro = _context.Livros.FirstOrDefault(livro => livro.Id == id);
        if (livro == null) return NotFound();
        var livroDTO = _mapper.Map<ReadLivroDTO>(livro);
        return Ok(livroDTO);
    }

    [HttpGet]
    public IEnumerable<ReadLivroDTO> ListarLivros()
    {
        return _mapper.Map<List<ReadLivroDTO>>(_context.Livros);
    }

    [HttpPut("{Id}")]
    public IActionResult AtualizarLivro(int Id, [FromBody] UpdateLivroDTO livroDTO)
    {
        var livro = _context.Livros.FirstOrDefault(livro => livro.Id == Id);
        if (livro == null) return NotFound();
        _mapper.Map(livroDTO, livro);
        _context.SaveChanges();
        return NoContent();
    }

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
