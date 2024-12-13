using DeltaGroupPricingApplication.Models.Entities;
using DeltaGroupPricingApplication.Models.Responses;
using DeltaGroupPricingApplication.Enums;

namespace DeltaGroupPricingApplication.Interfaces;

public interface IPrintingService
{
    /// <summary>
    /// Processes a printing job by calculating base price, discounts, additional costs (lamination, expedited delivery), and total price
    /// </summary>
    /// <param name="jobType">The type of job (Flyer, Poster, Banner, etc.)</param>
    /// <param name="quantity">The quantity of items for the job</param>
    /// <param name="isExpedited">Whether expedited delivery is required</param>
    /// <param name="isLaminated">Whether lamination is required</param>
    /// <returns>A response containing the processed printing job information</returns>
    PrintingServiceResponse<PrintingJobs> ProcessPrintingJob(JobType jobType, int quantity, bool isExpedited, bool isLaminated);

    /// <summary>
    /// Retrieves a printing job by its ID
    /// </summary>
    /// <param name="jobId">The ID of the printing job to retrieve</param>
    /// <returns>The printing job with the specified ID</returns>
    PrintingJobs GetPrintingJobById(int jobId);
}
