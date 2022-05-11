using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using final.Models;


namespace final.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<veiculosParaVenda> veiculosParaVenda { get; set; }
        public DbSet<final.Models.Categoria> Categoria { get; set; }
        public DbSet<final.Models.Licitacoes> Licitacoes { get; set; }
    }
}