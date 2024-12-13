using DeltaGroupPricingApplication.Enums;

namespace DeltaGroupPricingApplication.Interfaces;

public interface IAdditionalCostsService
{
    decimal ApplyLaminationCost(JobType jobType, int quantity);
    decimal ApplyExpeditedDeliveryCost(JobType jobType, int quantity);
}