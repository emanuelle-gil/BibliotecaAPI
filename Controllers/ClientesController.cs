using AutoMapper;
using BibliotecaAPI.Data;
using BibliotecaAPI.Data.Dtos;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ClientesController : ControllerBase
{
    private UsuarioDbContext _context;
    private IMapper _mapper;
    private EmprestimoService _emprestimoService;

    public ClientesController(UsuarioDbContext context, IMapper mapper, EmprestimoService emprestimoService)
    {
        _context = context;
        _mapper = mapper;
        _emprestimoService = emprestimoService;
    }

    [HttpPost]
     public IActionResult AdicionaCliente([FromBody] CreateClienteDTO clienteDTO)
     {
         Cliente cliente = _mapper.Map<Cliente>(clienteDTO);
         _context.Clientes.Add(cliente);
         _context.SaveChanges();
         return CreatedAtAction(nameof(BuscaCliente),
     new { nome = cliente.Nome },
     cliente);

     }

    [HttpGet("{Id}")]
    public IActionResult BuscaCliente(int Id)
    {
        var cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == Id);
        if (cliente == null) return NotFound();
        var clienteDTO = _mapper.Map<ReadClienteDTO>(cliente);
        return Ok(clienteDTO);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReadClienteDTO>> ListarClientes()
    {
        var clientes = _context.Clientes
                               .Include(c => c.LivrosEmprestados)
                               .ThenInclude(e => e.Livro)
                               .ToList();
        var readClientes = _mapper.Map<List<ReadClienteDTO>>(clientes);

        return Ok(readClientes); 
    }

    [HttpPut("{Id}")]
    public IActionResult AtualizarCliente(int Id, [FromBody] UpdateClienteDTO clienteDTO)
    {
        var cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == Id);
        if (cliente == null) return NotFound();
        _mapper.Map(clienteDTO, cliente);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{Id}")]
    public IActionResult DeletaCliente(int id)
    {
        var cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == id);
        if (cliente == null) return NotFound();
        _context.Remove(cliente);
        _context.SaveChanges();
        return NoContent(); 
    }

    [HttpPost("{clienteId}/emprestimos/{livroId}")]
    public IActionResult EmprestarLivro(int clienteId, int livroId)
    {
        var resultado = _emprestimoService.EmprestarLivro(clienteId, livroId);
        if (resultado.Contains("Erro")) 
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpPost("{clienteId}/emprestimos/{livroId}/devolucao")]
    public IActionResult DevolverLivro(int clienteId, int livroId)
    {
        var resultado = _emprestimoService.DevolverLivro(clienteId, livroId);
        if (resultado.Contains("Erro")) 
            return BadRequest(resultado);

        return Ok(resultado);
    }
}
