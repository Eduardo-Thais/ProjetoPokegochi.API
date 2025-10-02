using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoPokegochi.Data.Dtos;
using ProjetoPokegochi.Model;

namespace ProjetoPokegochi.Services
{
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

        public async Task<IdentityResult> Cadastra(CreateUsuarioDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);

            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

            

            return resultado;
        }

        public async Task<string> Login(LoginUsuarioDto dto)
        {
            var resultadoLogin = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if (!resultadoLogin.Succeeded)
                return string.Empty;

            var usuario = await _signInManager.UserManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == dto.Username.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
    }
}
