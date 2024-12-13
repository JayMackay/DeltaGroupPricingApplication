using DeltaGroupPricingApplication.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeltaGroupPricingApplication.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PrintingJobs> PrintingJobs { get; set; }
}