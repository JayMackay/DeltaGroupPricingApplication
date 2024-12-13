# Printing Job Pricing Calculations

This document outlines the calculations and architecture used to process printing job prices within the **DeltaGroupPricingApplication**.

## Architecture Overview

### Modular Services
- **Discount Service:** All bulk discount logic is handled in the `IDiscountService` implementation. This ensures modularity and scalability. Additional discount rules can be easily added to this service without modifying the core pricing logic.
- **Additional Costs Service:** Costs such as lamination and expedited delivery are encapsulated in the `IAdditionalCostsService`. This keeps the pricing logic clean and allows for centralized management of additional costs.

### JobType Enum
The `JobType` enum defines the different types of printing jobs (e.g., `Flyer`, `Poster`, `Banner`). Each job type has a specific base price defined via extension methods.

- **To add a new job type:**
  1. Extend the `JobType` enum with a new entry.
  2. Update the extension methods to define the base price and job name for the new type.

### Future Improvements
If more time were available, the `JobType` logic could be replaced with a database-driven model. A `JobType` table would allow:
- Defining job types and their associated data directly in the database.
- Dynamically updating job types and their prices without requiring code changes.
- Improved flexibility for managing job types in a production environment.

---

## Pricing Calculations

1. **Base Price**
   - Derived from the `JobType` and multiplied by the `quantity` to calculate the **total base price** before discounts.

2. **Discounts**
   - Bulk discounts are applied through the `IDiscountService`. Discounts are subtracted from the total base price *before* adding additional costs.

3. **Additional Costs**
   - **Lamination Cost:** Applied per unit if `isLaminated` is true.
   - **Expedited Delivery Cost:** Applied per unit if `isExpedited` is true.

4. **Total Price**
   - `totalPrice = (basePriceTotal - discount) + laminationCost + expeditedDeliveryCost`

---
