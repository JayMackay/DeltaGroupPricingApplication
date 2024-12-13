namespace DeltaGroupPricingApplication.Models.Responses;

public class PrintingServiceResponse<T>
{
    public T Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public bool DiscountApplied { get; set; }

    public PrintingServiceResponse(T data, bool success, string message, bool discountApplied = false)
    {
        Data = data;
        Success = success;
        Message = message;
        DiscountApplied = discountApplied;
    }
}
