using Microsoft.EntityFrameworkCore;
using PropostaService.Persistency.Entities;

namespace PropostaService.Persistency.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }


        public virtual DbSet<Proposta>? Propostas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddInitialData(modelBuilder);
        }

        private void AddInitialData(ModelBuilder modelBuilder)
        {
        }
    }
}
