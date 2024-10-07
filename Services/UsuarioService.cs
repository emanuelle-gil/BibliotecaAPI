using AutoMapper;
using BibliotecaAPI.Data.Dtos;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace BibliotecaAPI.Services;

public class UsuarioService
{
    private IMapper _mapper;
    private UserManager<Usuario> _userManager;
    private SignInManager<Usuario> _signInManager;
    private TokenService _tokenService;

    public UsuarioService(UserManager<Usuario> userManager, IMapper mapper, SignInManager<Usuario> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task Cadastra(CreateUsuarioDto dto)
    {
        Usuario usuario = _mapper.Map<Usuario>(dto);
        usuario.UserName = dto.CPF;
        IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Senha);
        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar usuário!");
        }

    }

    public async Task<string> Login(LoginUsuarioDTO dto)
    {
        var resultado = await 
            _signInManager.PasswordSignInAsync
            (dto.CPF, dto.Senha, false, false);

        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Usuário não autenticado!");
        }

        var usuario = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.CPF.ToUpper());
        var token = _tokenService.GenerateToken(usuario);
        return token;
    }
}
