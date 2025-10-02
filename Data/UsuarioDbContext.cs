using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoPokegochi.Model;


public class UsuarioDbContext : IdentityDbContext<Usuario>
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> opts) :
        base(opts)  {  }
        

    //public DbSet<ProjetoPokegochi.Model.Clientes> Clientes { get; set; } = default!;
}
