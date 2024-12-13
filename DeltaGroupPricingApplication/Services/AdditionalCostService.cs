using DeltaGroupPricingApplication.Enums;
using DeltaGroupPricingApplication.Interfaces;

namespace DeltaGroupPricingApplication.Services
{
    public class AdditionalCostsService : IAdditionalCostsService
    {
        private static readonly Dictionary<JobType, decimal> LaminationCosts = new Dictionary<JobType, decimal>
        {
            // Additional JobTypes & their lamination costs can be added here
            { JobType.Poster, 2.00m },
            
        };

        public decimal ApplyLaminationCost(JobType jobType, int quantity)
        {
            if(LaminationCosts.ContainsKey(jobType))
            {
                return LaminationCosts[jobType] * quantity;
            }

            return 0.00m;
        }

        public decimal ApplyExpeditedDeliveryCost(JobType jobType, int quantity)
        {
            decimal expeditedDeliveryCost = 0;

            // Expedited delivery costs based on job type and quantity
            switch(jobType)
            {
                case JobType.Flyer:
                    expeditedDeliveryCost = 1.00m * quantity;
                    break;

                case JobType.Poster:
                    expeditedDeliveryCost = 2.50m * quantity;
                    break;

                case JobType.Banner:
                    expeditedDeliveryCost = 5.00m * quantity;
                    break;

                // Additional logic for other job types can be added here
                default:
                    break;
            }

            return expeditedDeliveryCost;
        }
    }
}