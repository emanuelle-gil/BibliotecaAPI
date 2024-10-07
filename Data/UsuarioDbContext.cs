using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Data;

public class UsuarioDbContext : IdentityDbContext<Usuario>
{
    public UsuarioDbContext(DbContextOptions<UsuarioDbContext> opts) :
        base(opts)
    {
        
    }
    
    public DbSet<Livro> Livros { get; set; }
    public DbSet<Cliente> Clientes { get; set;}
}
