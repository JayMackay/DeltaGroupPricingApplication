using DeltaGroupPricingApplication.Enums;

namespace DeltaGroupPricingApplication.Services;

public class DiscountService : IDiscountService
{
    public decimal ApplyBulkDiscount(JobType jobType, decimal basePrice, int quantity)
    {
        switch(jobType)
        {
            case JobType.Flyer:
                if(quantity >= 100)
                    return basePrice * quantity * 0.5m; 
                break;

            case JobType.Poster:
                if(quantity >= 50)
                    return basePrice * quantity * 0.8m;
                break;

            case JobType.Banner:
                if(quantity >= 20)
                    return basePrice * quantity * 0.9m;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(jobType), jobType, null);
        }

        return basePrice;
    }
}
