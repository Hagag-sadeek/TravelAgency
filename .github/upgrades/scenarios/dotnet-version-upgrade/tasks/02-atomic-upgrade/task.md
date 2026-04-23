# 02-atomic-upgrade: Upgrade Project and Dependencies

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
