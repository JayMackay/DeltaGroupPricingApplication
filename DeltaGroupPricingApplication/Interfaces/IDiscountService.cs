using DeltaGroupPricingApplication.Enums;

namespace DeltaGroupPricingApplication.Services;

public interface IDiscountService
{
    decimal ApplyBulkDiscount(JobType jobType, decimal basePrice, int quantity);
}
