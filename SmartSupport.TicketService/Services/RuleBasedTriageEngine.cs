using SmartSupport.TicketService.Models;

namespace SmartSupport.TicketService.Services
{
    public class RuleBasedTriageEngine : ITicketTriageEngine
    {
        public TriageResult Triage(Ticket ticket)
        {
            TicketTriageService.ApplyRules(ticket);

            return new TriageResult
            {
                Category = ticket.Category,
                Priority = ticket.Priority,
                Confidence = ticket.TriageConfidence,
                Explanation = ticket.TriageExplanation,
                Source = "rules"
            };
        }
    }
}
