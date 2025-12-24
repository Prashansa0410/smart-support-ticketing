using System;

namespace SmartSupport.TicketService.Models
{
    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public double TriageConfidence { get; set; } = 0.0;

        public string Description { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string Status { get; set; } = "open"; // Default value to fix nullable warning
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
         public TriageExplainability TriageExplainability { get; set; }
        public string TriageExplanation { get; set; } = string.Empty;
        public string Category { get; set; } = "general";
        public string Priority { get; set; } = "low";
        public DateTime? UpdatedAt { get; set; } // Nullable timestamp for updates
    }
}