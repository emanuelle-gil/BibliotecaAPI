
using BibliotecaAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BibliotecaAPI.Services;

public class TokenService
{
    public string GenerateToken(Usuario usuario)
    {
        Claim[] claims = new Claim[]
        {
        new Claim("cpf", usuario.CPF),
        new Claim("DataNascimento", usuario.DataNascimento.ToString("yyyy-MM-dd")),
        new Claim("loginTimestamp", DateTime.UtcNow.ToString())
        };
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("edh84ejd4dj4d84h793849348jsk947rhd4eu"));
        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}