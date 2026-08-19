using BillingApi.Clients;
using BillingApi.Data;
using BillingApi.Repositories;
using BillingApi.Services;
using BillingApi.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<BillingDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceItemService, InvoiceItemService>();
builder.Services.AddScoped<IStockClient, StockClient>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddHttpClient("StockApi", client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["Services:StockApi"]!
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseCors("Angular");

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();