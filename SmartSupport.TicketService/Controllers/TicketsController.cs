using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSupport.TicketService.Data;
using SmartSupport.TicketService.Models;
using SmartSupport.TicketService.Services; // 🔹 Step 13
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSupport.TicketService.Controllers
{
    [ApiController]
    [Route("tickets")]
    public class TicketsController : ControllerBase
    {
        private readonly TicketDbContext _context;

        public TicketsController(TicketDbContext context)
        {
            _context = context;
        }

        // =========================
        // POST /tickets  (Step 11–13)
        // =========================
        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] Ticket ticket)
        {
            // 1. Validate input
            if (string.IsNullOrWhiteSpace(ticket.Title) ||
                string.IsNullOrWhiteSpace(ticket.Description) ||
                string.IsNullOrWhiteSpace(ticket.CustomerEmail))
            {
                return BadRequest("Title, Description, and CustomerEmail are required.");
            }

            // 2. Core ticket initialization (Step 11–12)
            ticket.Id = Guid.NewGuid();
            ticket.Status = "open";
            ticket.CreatedAt = DateTime.UtcNow;

            // 3. 🔹 STEP 13 — Rule-based triage
            TicketTriageService.ApplyRules(ticket);

            // 4. Persist ticket
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            // 5. Return response
            return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, new
            {
                id = ticket.Id,
                status = ticket.Status,
                category = ticket.Category,
                priority = ticket.Priority,
                triageConfidence = ticket.TriageConfidence,
                triageExplanation = ticket.TriageExplanation,
                createdAt = ticket.CreatedAt
            });
        }

        // =========================
        // GET /tickets
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _context.Tickets
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new
                {
                    t.Id,
                    t.Title,
                    t.Status,
                    t.Category,
                    t.Priority,
                    t.TriageConfidence,
                    t.TriageExplanation,
                    t.CreatedAt
                })
                .ToListAsync();

            return Ok(tickets);
        }

        // =========================
        // GET /tickets/{id}
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        // =========================
        // PUT /tickets/{id}/status  (Step 12)
        // =========================
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusRequest request)
        {
            var allowedStatuses = new[] { "open", "in_progress", "resolved" };

            if (!allowedStatuses.Contains(request.Status))
            {
                return BadRequest(new { error = "Invalid status value." });
            }

            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound(new { error = "Ticket not found." });
            }

            ticket.Status = request.Status;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                id = ticket.Id,
                status = ticket.Status,
                updatedAt = ticket.UpdatedAt
            });
        }
    }

    // =========================
    // Request DTO
    // =========================
    public class UpdateStatusRequest
    {
        public string Status { get; set; }
    }
}
