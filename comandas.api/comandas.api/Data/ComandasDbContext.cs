using comandas.api.Domain;
using Microsoft.EntityFrameworkCore;

namespace comandas.api.Data
{
    public class ComandasDbContext : DbContext
    {
        public DbSet<CardapioItem> CardapioItems { get; set; }
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<ComandaItem> ComandaItems { get; set; }
        public DbSet<Mesa> Mesas { get; set; }  
        public DbSet<PedidoCozinha> PedidoCozinhas { get; set; }
        public DbSet<PedidoCozinhaItem> PedidoCozinhaItems { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public ComandasDbContext(DbContextOptions<ComandasDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CardapioItem>()
                .Property(c => c.Preco)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Comanda>()
                .Property(c => c.NomeCliente)
                .HasColumnType("varchar(200)");

        }
    }
}
