using Application.Services;
using Domain.Interfaces;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebSocket;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddFastEndpoints();
builder.Services.AddSingleton<IPriceProvider, PriceProvider>();
builder.Services.AddSingleton<PriceService>();
builder.Services.AddSingleton<TiingoPriceService>();
builder.Services.AddFastEndpoints().SwaggerDocument();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
	endpoints.MapHub<AmegaHub>("/amegahub");
	endpoints.MapFallbackToFile("/Client/index.html");
});
app.UseFastEndpoints().UseSwaggerGen();

// Start the TiingoPriceService
var priceService = app.Services.GetRequiredService<TiingoPriceService>();
priceService.Start();

app.Run();