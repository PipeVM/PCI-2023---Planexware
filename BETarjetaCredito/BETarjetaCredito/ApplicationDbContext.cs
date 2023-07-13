using BETarjetaCredito.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BETarjetaCredito
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TarjetaCredito> TarjetaCredito { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}