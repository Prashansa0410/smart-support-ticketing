using SmartSupport.TicketService.Models;

namespace SmartSupport.TicketService.Services
{
    public interface ITicketTriageEngine
    {
        TriageResult Triage(Ticket ticket);
    }
}
