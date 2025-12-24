using SmartSupport.TicketService.Models;

namespace SmartSupport.TicketService.Services
{
    public class MlTriageEngine : ITicketTriageEngine
    {
        public TriageResult Triage(Ticket ticket)
        {
            // Placeholder for future ML model
            return new TriageResult
            {
                Category = "general",
                Priority = "low",
                Confidence = 0.5,
                Explanation = "ML model placeholder",
                Source = "ml"
            };
        }
    }
}
