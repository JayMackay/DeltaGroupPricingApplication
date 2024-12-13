using Microsoft.AspNetCore.Mvc;
using DeltaGroupPricingApplication.Models.Entities;
using DeltaGroupPricingApplication.Models.Responses;
using DeltaGroupPricingApplication.Enums;
using DeltaGroupPricingApplication.Services;
using DeltaGroupPricingApplication.Interfaces;

namespace DeltaGroupPricingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintingController : ControllerBase
    {
        private readonly IPrintingService _printingService;

        public PrintingController(IPrintingService printingService)
        {
            _printingService = printingService;
        }

        [HttpPost("processPrintingJob")]
        public IActionResult ProcessPrintingJob([FromBody] PrintingJobRequest request)
        {
            try
            {
                // Process the printing job
                var response = _printingService.ProcessPrintingJob(
                    request.JobType,
                    request.Quantity,
                    request.IsExpedited,
                    request.IsLaminated
                );

                // Check if the response was successful
                if(response.Success)
                {
                    return Ok(response);
                }

                // Return a 400 Bad Request if the process failed
                return BadRequest(new { message = response.Message, job = response.Data });
            }
            catch(Exception ex)
            {
                // Return a 500 Internal Server Error if an exception occurred
                var errorResponse = new PrintingServiceResponse<PrintingJobs>(
                    new PrintingJobs
                    {
                        JobType = JobType.Error,  // Set JobType as Error for failed job
                        Quantity = 0,
                        BasePrice = 0.00m,
                        TotalPrice = 0.00m,
                        DiscountAmount = 0.00m,
                        IsExpedited = false,
                        IsLaminated = false,
                        OrderDate = DateTime.UtcNow
                    },
                    false,
                    $"Error processing the job: {ex.Message}"
                );
                return StatusCode(500, errorResponse); // Internal Server Error response
            }
        }

        [HttpGet("getPrintingJobById/{id}")]
        public IActionResult GetPrintingJobById(int id)
        {
            try
            {
                var job = _printingService.GetPrintingJobById(id);
                return Ok(job); // Return the job if found
            }
            catch(Exception ex)
            {
                // Return an error response if the job was not found
                return NotFound(new { message = $"Job not found: {ex.Message}" });
            }
        }
    }

    // The request model to receive job details
    public class PrintingJobRequest
    {
        public JobType JobType { get; set; }
        public int Quantity { get; set; }
        public bool IsExpedited { get; set; }
        public bool IsLaminated { get; set; }
    }
}