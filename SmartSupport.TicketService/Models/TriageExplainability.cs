public class TriageExplainability
{
    public double Confidence { get; set; }
    public string Method { get; set; } // "rules" | "ml"
    public List<TriageSignal> Signals { get; set; } = new();
}

public class TriageSignal
{
    public string Name { get; set; }
    public string Value { get; set; }
    public double Weight { get; set; }
    public string Reason { get; set; }
}
