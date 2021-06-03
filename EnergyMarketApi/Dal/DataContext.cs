using EnergyMarketApi.Model.Dto;
using Microsoft.EntityFrameworkCore;

namespace EnergyMarketApi.Dal
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public virtual DbSet<EnergyHistoryDto> Energy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnergyHistoryDto>(entity =>
            {
                entity.HasKey(e => e.Uuid);
            });
        }
    }
}