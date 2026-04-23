# .NET Version Upgrade Plan

## Overview

**Target**: TravelAgency.csproj (Razor Pages application)  
**Scope**: Single project upgrade from .NET 6 to .NET 10.0 (LTS), ~21,500 LOC

### Selected Strategy
**All-At-Once** — All project components upgraded simultaneously in a single operation.  
**Rationale**: 1 project, straightforward upgrade, clear package compatibility, low-risk changes.

## Tasks

### 01-prerequisites: Validate Prerequisites

Verify the environment is ready for .NET 10.0 upgrade:
- Confirm .NET 10 SDK is installed
- Validate global.json compatibility (if present)
- Check for IDE/tooling compatibility

**Done when**: .NET 10 SDK verified available, no blocking prerequisites identified.

---

### 02-atomic-upgrade: Upgrade Project and Dependencies

Upgrade the TravelAgency project in one atomic operation:
- Update TargetFramework from net6.0 to net10.0
- Update all Entity Framework Core packages (6.0.0 → 10.0.7)
- Update Microsoft.VisualStudio.Web.CodeGeneration.Design (6.0.0 → 10.0.2)
- Update RestSharp (111.4.1 → 114.0.0) to resolve security vulnerability
- Remove Microsoft.AspNetCore.Razor 2.2.0 (functionality included in framework)
- Restore dependencies
- Fix any compilation errors in one pass

**Key concerns**:
- 1 source-incompatible API (System.TimeSpan.FromMinutes) - verify behavior
- RestSharp security vulnerability must be resolved
- Razor Pages patterns may need adjustments for .NET 10

**Done when**: Project targets net10.0, all packages updated, solution builds with 0 errors.

---

### 03-validation: Validate Upgrade

Verify the upgraded application works correctly:
- Build solution (must succeed with 0 errors)
- Run all tests (if test project exists)
- Verify Razor Pages application runs without runtime errors
- Confirm database connectivity (Entity Framework)
- Check for breaking behavior changes

**Done when**: All tests pass (or build succeeds if no tests), application runs without errors, core functionality verified.
