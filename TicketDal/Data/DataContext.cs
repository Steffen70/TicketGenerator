using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TicketDal.Entities;
using TicketDal.Helpers;

namespace TicketDal.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Write Fluent API configurations here

            builder.ApplyUtcDateTimeConverter();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(S.I.ConnectionString);
        }
    }
}