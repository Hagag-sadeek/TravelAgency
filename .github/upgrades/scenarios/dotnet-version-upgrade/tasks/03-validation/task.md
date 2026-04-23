# 03-validation: Validate Upgrade

Verify the upgraded application works correctly:
- Build solution (must succeed with 0 errors)
- Run all tests (if test project exists)
- Verify Razor Pages application runs without runtime errors
- Confirm database connectivity (Entity Framework)
- Check for breaking behavior changes

**Done when**: All tests pass (or build succeeds if no tests), application runs without errors, core functionality verified.
