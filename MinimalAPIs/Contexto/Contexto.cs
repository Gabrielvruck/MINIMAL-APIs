using Microsoft.EntityFrameworkCore;
using MinimalAPIs.Models;

namespace MinimalAPIs.Contexto
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options) => Database.EnsureCreated();
        //EnsureCreated verifica se existe aquele banco

        public DbSet<Produto> Produto { get; set; }
    }
}
