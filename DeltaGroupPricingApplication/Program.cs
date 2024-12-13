using DeltaGroupPricingApplication.Data;
using DeltaGroupPricingApplication.Interfaces;
using DeltaGroupPricingApplication.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add In Memory Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("PrintingJobsDb"));

// Register Custom services
builder.Services.AddScoped<IPrintingService, PrintingService>();
builder.Services.AddScoped<IAdditionalCostsService, AdditionalCostsService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
