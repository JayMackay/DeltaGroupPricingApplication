using DeltaGroupPricingApplication.Data;
using DeltaGroupPricingApplication.Models.Entities;
using DeltaGroupPricingApplication.Models.Responses;
using DeltaGroupPricingApplication.Enums;
using System;
using DeltaGroupPricingApplication.Interfaces;

namespace DeltaGroupPricingApplication.Services;

public class PrintingService : IPrintingService
{
    private readonly IAdditionalCostsService _additionalCostsService;
    private readonly IDiscountService _discountService;
    private readonly AppDbContext _context;

    public PrintingService(IAdditionalCostsService additionalCostsService, IDiscountService discountService, AppDbContext context)
    {
        _additionalCostsService = additionalCostsService;
        _discountService = discountService;
        _context = context;
    }

    public PrintingServiceResponse<PrintingJobs> ProcessPrintingJob(JobType jobType, int quantity, bool isExpedited, bool isLaminated)
    {
        try
        {
            decimal unitBasePrice = jobType.GetBasePrice();

            // Calculate total price before discount is applied
            decimal basePriceTotal = unitBasePrice * quantity;

            // Apply bulk discount based on job type and quantity
            decimal discountedPrice = _discountService.ApplyBulkDiscount(jobType, unitBasePrice, quantity);
            bool discountApplied = discountedPrice > 0; // Set discountApplied to true if discount is applied

            decimal dicountAmount = basePriceTotal - discountedPrice;

            // Apply additional costs (lamination and expedited delivery)
            decimal laminationCost = isLaminated ? _additionalCostsService.ApplyLaminationCost(jobType, quantity) : 0.00m;
            decimal expeditedDeliveryCost = isExpedited ? _additionalCostsService.ApplyExpeditedDeliveryCost(jobType, quantity) : 0.00m;

            // Calculate the total price (after applying the discount and adding extra costs)
            decimal totalPrice = discountedPrice + laminationCost + expeditedDeliveryCost;

            // Create and save the printing job
            var printingJob = new PrintingJobs
            {
                JobType = jobType,
                Quantity = quantity,
                BasePrice = basePriceTotal,
                TotalPrice = totalPrice,     
                DiscountAmount = dicountAmount,      
                IsExpedited = isExpedited,
                IsLaminated = isLaminated,
                OrderDate = DateTime.UtcNow
            };

            // Database Save Logic (Any additional logic can be separated into its own repository layer)
            _context.PrintingJobs.Add(printingJob);
            _context.SaveChanges();

            // Return response with the printing job data, success status, and whether the discount was applied
            return new PrintingServiceResponse<PrintingJobs>(printingJob, true, "Printing job processed and saved successfully", discountApplied);
        }
        catch(Exception ex)
        {
            // Return default job when an exception occurs
            var emptyJob = new PrintingJobs
            {
                Id = 0,
                JobType = JobType.Error,
                Quantity = 0,
                BasePrice = 0.00m,
                TotalPrice = 0.00m,
                DiscountAmount = 0.00m,
                IsExpedited = false,
                IsLaminated = false,
                OrderDate = DateTime.UtcNow
            };

            // Return a failure response with the error message
            return new PrintingServiceResponse<PrintingJobs>(emptyJob, false, $"Error processing job: {ex.Message}");
        }
    }

    public PrintingJobs GetPrintingJobById(int jobId)
    {
        try
        {
            var job = _context.PrintingJobs.Find(jobId);
            if(job == null)
                throw new Exception("Job not found");

            return job;
        }
        catch(Exception ex)
        {
            throw new Exception($"Error fetching job: {ex.Message}");
        }
    }
}