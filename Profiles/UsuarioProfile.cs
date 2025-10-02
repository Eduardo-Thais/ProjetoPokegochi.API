using AutoMapper;
using ProjetoPokegochi.Data.Dtos;
using ProjetoPokegochi.Model;

namespace ProjetoPokegochi.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}
