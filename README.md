A full-stack smart support ticketing system with **rule-based triage, explainable decisions, confidence scoring,** and a **safe ML shadow-mode pipeline**.
Designed to demonstrate **real-world system evolution from rules → ML**.

**Highlights**

End-to-end ticket management
Deterministic rule-based triage (authoritative)
Explainable decision signals (UI + backend)
Confidence scoring for every decision
ML classifier running in shadow mode
Safe, incremental ML adoption (no production risk)


Next.js UI
   ↓
ASP.NET Core API
   ↓
TicketsController
   ↓
TicketTriageService
   ├─ Rule Engine (authoritative)
   ├─ Explainability Engine
   └─ Shadow ML Classifier (read-only)
   ↓
SQLite Database

**Key principle:**

**Ticket Lifecycle**

Create, view, and update tickets
Status flow: open → in_progress → resolved
Multiple ticket types:
Login issue
Billing issue
Bug report
Feature request
General query

**Rule-Based Triage**

Automatic category & priority assignment
Deterministic, auditable logic
Designed as the system’s source of truth

**Explainability**
Every triage decision includes:
Signals used
Weights applied
Human-readable reasons
Displayed directly in the UI for transparency.

**Confidence Scoring**
Confidence derived from decision signals
Used for:
Explainability
ML evaluation
Future rollout gating

**ML Shadow Mode**
ML classifier runs in parallel with rules
ML predictions are:
Logged
Compared
Never applied to production behavior
Enables:
Safe evaluation
Disagreement analysis
Training data growth

**ML Strategy (High-Level)**
Uses first-party ticket data only
Initial labels from rules + human validation
Model trained using:
TF-IDF
Logistic Regression
ML introduced via shadow mode
Designed for future confidence-gated rollout

**Tech Stack**
**Backend**
ASP.NET Core (.NET 8)
Entity Framework Core
SQLite
Structured logging
ONNX-ready ML integration

**Frontend**
Next.js (React)
Tailwind CSS
shadcn/ui
Explainability-aware UI components

**DevOps**
GitHub Actions CI
Monorepo (backend + UI)
Environment-safe configuration

**Getting Started**

**Backend**
cd SmartSupport.TicketService
dotnet restore
dotnet ef database update
dotnet run

Runs at:
http://localhost:5000

**UI**
cd smartsupport.ui
npm install
npm run dev

Runs at:
http://localhost:3000

**Environment Variables**
UI (.env.local)
NEXT_PUBLIC_API_BASE_URL=http://localhost:5000

**Roadmap**

Rule-based triage
Explainability UI
Confidence scoring
ML shadow mode
CI pipeline
Confidence-gated ML rollout
Drift detection
Admin analytics dashboard

📄 License
MIT License
