using AutoMapper;
using BibliotecaAPI.Data;
using BibliotecaAPI.Data.Dtos;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientesController : ControllerBase
{
    private UsuarioDbContext _context;
    private IMapper _mapper;

    public ClientesController(UsuarioDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaCliente([FromBody] CreateClienteDTO clienteDTO)
    {
        Cliente cliente = _mapper.Map<Cliente>(clienteDTO);
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaCliente)
    new { nome = cliente.Nome },
    cliente);
    }

    [HttpGet("{Nome}")]
    public IActionResult BuscaCliente(string Nome)
    {
        var cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Nome == Nome);
        if (cliente == null) return NotFound();
        var clienteDTO = _mapper.Map<ReadClienteDTO>(cliente);
        return Ok(clienteDTO);
    }

    [HttpGet]
    public IEnumerable<ReadClienteDTO> ListarClientes()
    {
        return _mapper.Map<List<ReadClienteDTO>>(_context.Clientes);
    }

    [HttpPut("{Nome}")]
    public IActionResult AtualizarCliente(string Nome, [FromBody] UpdateClienteDTO clienteDTO)
    {
        var cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Nome == Nome);
        if (cliente == null) return NotFound();
        _mapper.Map(clienteDTO, cliente);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{Nome}")]
    public IActionResult DeletaCliente(string Nome)
    {
        var cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Nome == Nome);
        if (cliente == null) return NotFound();
        _context.Remove(cliente);
        _context.SaveChanges();
        return NoContent(); 
    }
}
