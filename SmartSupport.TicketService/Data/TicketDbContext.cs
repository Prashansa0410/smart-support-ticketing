using Microsoft.EntityFrameworkCore;
using SmartSupport.TicketService.Models;

namespace SmartSupport.TicketService.Data
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
