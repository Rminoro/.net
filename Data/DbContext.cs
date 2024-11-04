using Microsoft.EntityFrameworkCore;
using Sprint4dotnet.Models;

namespace Sprint4dotnet.Data
{
    public class YourDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public YourDbContext(DbContextOptions<YourDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=ORCL)));User Id=RM99988;Password=160698;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(b =>
            {
                b.ToTable("Clients"); 
                b.HasKey(c => c.Id);
                b.Property(c => c.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("NUMBER(10)");
                b.Property(c => c.Email)
                    .IsRequired()
                    .HasColumnType("VARCHAR2(255)");
                b.Property(c => c.Password)
                    .IsRequired()
                    .HasColumnType("VARCHAR2(255)");
                b.Property(c => c.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR2(255)");
            });
        }
    }
}
