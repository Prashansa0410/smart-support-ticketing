namespace SmartSupport.TicketService.Models
{
    public class TriageResult
    {
        public string Category { get; set; } = "general";
        public string Priority { get; set; } = "low";
        public double Confidence { get; set; }
        public string Explanation { get; set; } = string.Empty;
        public string Source { get; set; } = "rules"; // rules | ml
    }
}
