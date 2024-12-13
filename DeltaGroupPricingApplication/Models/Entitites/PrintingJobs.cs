using DeltaGroupPricingApplication.Enums;

namespace DeltaGroupPricingApplication.Models.Entities;

public class PrintingJobs
{
    public int Id { get; set; }
    public JobType JobType { get; set; }
    public int Quantity { get; set; }
    public decimal BasePrice { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsExpedited { get; set; }
    public bool IsLaminated { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal DiscountAmount { get; set; }
}
