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

    /// <summary>
    /// Adiciona um novo cliente ao sistema.
    /// </summary>
    /// <param name="clienteDTO">Dados do cliente a ser adicionado.</param>
    /// <returns>Um status 201 Created se o cliente for adicionado com sucesso.</returns>
    /// <response code="201">Cliente adicionado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost]
    public IActionResult AdicionaCliente([FromBody] CreateClienteDTO clienteDTO)
    {
        Cliente cliente = _mapper.Map<Cliente>(clienteDTO);
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscaCliente), new { id = cliente.Id }, cliente);
    }

    /// <summary>
    /// Busca um cliente pelo ID.
    /// </summary>
    /// <param name="Id">ID do cliente a ser buscado.</param>
    /// <returns>Um objeto cliente se encontrado.</returns>
    /// <response code="200">Cliente encontrado.</response>
    /// <response code="404">Cliente não encontrado.</response>
    [HttpGet("{Id}")]
    public IActionResult BuscaCliente(int Id)
    {
        var cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == Id);
        if (cliente == null) return NotFound();
        var clienteDTO = _mapper.Map<ReadClienteDTO>(cliente);
        return Ok(clienteDTO);
    }

    /// <summary>
    /// Lista todos os clientes cadastrados.
    /// </summary>
    /// <returns>Uma lista de clientes.</returns>
    /// <response code="200">Lista de clientes retornada com sucesso.</response>
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

    /// <summary>
    /// Atualiza os dados de um cliente existente.
    /// </summary>
    /// <param name="Id">ID do cliente a ser atualizado.</param>
    /// <param name="clienteDTO">Dados atualizados do cliente.</param>
    /// <returns>Um status 204 No Content se a atualização for bem-sucedida.</returns>
    /// <response code="204">Cliente atualizado com sucesso.</response>
    /// <response code="404">Cliente não encontrado.</response>
    [HttpPut("{Id}")]
    public IActionResult AtualizarCliente(int Id, [FromBody] UpdateClienteDTO clienteDTO)
    {
        var cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == Id);
        if (cliente == null) return NotFound();
        _mapper.Map(clienteDTO, cliente);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deleta um cliente do sistema.
    /// </summary>
    /// <param name="id">ID do cliente a ser deletado.</param>
    /// <returns>Um status 204 No Content se a deleção for bem-sucedida.</returns>
    /// <response code="204">Cliente deletado com sucesso.</response>
    /// <response code="404">Cliente não encontrado.</response>
    [HttpDelete("{Id}")]
    public IActionResult DeletaCliente(int id)
    {
        var cliente = _context.Clientes.FirstOrDefault(cliente => cliente.Id == id);
        if (cliente == null) return NotFound();
        _context.Remove(cliente);
        _context.SaveChanges();
        return NoContent(); 
    }

    /// <summary>
    /// Empresta um livro a um cliente.
    /// </summary>
    /// <param name="clienteId">ID do cliente que está emprestando o livro.</param>
    /// <param name="livroId">ID do livro a ser emprestado.</param>
    /// <returns>Uma mensagem de sucesso ou erro.</returns>
    /// <response code="200">Livro emprestado com sucesso.</response>
    /// <response code="400">Erro ao tentar emprestar o livro.</response>
    [HttpPost("{clienteId}/emprestimos/{livroId}")]
    public IActionResult EmprestarLivro(int clienteId, int livroId)
    {
        var resultado = _emprestimoService.EmprestarLivro(clienteId, livroId);
        if (resultado.Contains("Erro")) 
            return BadRequest(resultado);

        return Ok(resultado);
    }

    /// <summary>
    /// Devolve um livro emprestado por um cliente.
    /// </summary>
    /// <param name="clienteId">ID do cliente que está devolvendo o livro.</param>
    /// <param name="livroId">ID do livro a ser devolvido.</param>
    /// <returns>Uma mensagem de sucesso ou erro.</returns>
    /// <response code="200">Livro devolvido com sucesso.</response>
    /// <response code="400">Erro ao tentar devolver o livro.</response>
    [HttpPost("{clienteId}/emprestimos/{livroId}/devolucao")]
    public IActionResult DevolverLivro(int clienteId, int livroId)
    {
        var resultado = _emprestimoService.DevolverLivro(clienteId, livroId);
        if (resultado.Contains("Erro")) 
            return BadRequest(resultado);

        return Ok(resultado);
    }
}
