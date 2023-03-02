using Microsoft.EntityFrameworkCore;
using TicketDal.Entities;
using TicketDal.Settings;
using TicketGenerator.Helpers;

namespace TicketGenerator.Data
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
            optionsBuilder.UseMySQL(AppSettings.I.ConnectionString ?? throw new Exception("Configure the database connection string in your appsettings.json"));
        }
    }
}