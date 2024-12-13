namespace DeltaGroupPricingApplication.Enums;

public static class JobTypeExtensions
{
    public static decimal GetBasePrice(this JobType jobType)
    {
        switch(jobType)
        {
            case JobType.Flyer:
                return 5.0m;
            case JobType.Poster:
                return 7.0m;
            case JobType.Banner:
                return 10.0m;
            default:
                throw new ArgumentOutOfRangeException(nameof(jobType), jobType, "Job type not found in pricing dictionary.");
        }
    }

    public static string GetJobName(this JobType jobType)
    {
        return jobType.ToString();
    }
}
