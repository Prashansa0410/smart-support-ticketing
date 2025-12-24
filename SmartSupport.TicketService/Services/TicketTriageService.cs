using SmartSupport.TicketService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace SmartSupport.TicketService.Services
{
    public class TicketTriageService
    {
        private readonly ShadowMLClassifier _shadowMLClassifier;
        private readonly ILogger<TicketTriageService> _logger;

        public TicketTriageService(
            ShadowMLClassifier shadowMLClassifier,
            ILogger<TicketTriageService> logger)
        {
            _shadowMLClassifier = shadowMLClassifier;
            _logger = logger;
        }

        // 🔹 Step 17 — Rules + Shadow ML
        public void ApplyRulesAndShadowML(Ticket ticket)
        {
            // 1. Apply rules (authoritative)
            ApplyRules(ticket);

            // 2. Run ML in shadow mode
            var mlPrediction = _shadowMLClassifier.Predict(ticket);

            // 3. Log comparison (DO NOT APPLY ML RESULT)
            _logger.LogInformation(
                "Shadow ML prediction for ticket {TicketId}. ML: Category={MLCategory}, Priority={MLPriority}, Confidence={MLConfidence}. Rules: Category={RuleCategory}, Priority={RulePriority}",
                ticket.Id,
                mlPrediction.Category,
                mlPrediction.Priority,
                mlPrediction.Confidence,
                ticket.Category,
                ticket.Priority
            );
        }

        // 🔹 Step 13–16 — Rule-based triage
        private void ApplyRules(Ticket ticket)
        {
            var signals = new List<TriageSignal>();

            if (!string.IsNullOrWhiteSpace(ticket.Description) &&
                ticket.Description.Contains("password", StringComparison.OrdinalIgnoreCase))
            {
                ticket.Category = "Authentication";
                ticket.Priority = "High";

                signals.Add(new TriageSignal
                {
                    Name = "keyword_match",
                    Value = "password",
                    Weight = 0.4,
                    Reason = "Password-related keywords detected"
                });
            }

            if (!string.IsNullOrWhiteSpace(ticket.Description) &&
                ticket.Description.Contains("cannot login", StringComparison.OrdinalIgnoreCase))
            {
                signals.Add(new TriageSignal
                {
                    Name = "urgency_phrase",
                    Value = "cannot login",
                    Weight = 0.3,
                    Reason = "Urgent login issue detected"
                });
            }

            ticket.TriageExplainability = new TriageExplainability
            {
                Confidence = signals.Sum(s => s.Weight),
                Method = "rules",
                Signals = signals
            };
        }
    }
}
