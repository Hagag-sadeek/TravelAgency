# Scenario Instructions: .NET Version Upgrade

## Overview
Upgrade TravelAgency solution from .NET 6 to .NET 10.0 (LTS), then refactor to clean architecture.

## Preferences

### Flow Mode
**Automatic** — Run end-to-end, only pause when blocked or needing user input

### Technical Preferences
- **Target Framework**: .NET 10.0 (LTS)
- **Source Branch**: upgraded
- **Working Branch**: upgrade-to-NET10
- **Future Work**: After upgrade completes, refactor solution to clean architecture (Core/Application/Infrastructure/Presentation layers)

## Strategy

**Selected**: All-At-Once  
**Rationale**: 1 project, straightforward upgrade (TFM + package bumps), low difficulty rating, clear package compatibility

### Execution Constraints
- Single atomic upgrade — all components updated together
- Validate full solution build after upgrade completes
- No incremental commits — one complete upgrade operation

## Commit Strategy

**After Each Task** — Commit after each major task completes successfully

## Key Decisions Log

(Decisions made during execution will be recorded here)

## Custom Instructions

(Task-specific instructions will be added here as needed)