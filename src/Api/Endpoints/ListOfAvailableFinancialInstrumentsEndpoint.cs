using Application.Services;
using FastEndpoints;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Api.Endpoints
{
    public class ListOfAvailableFinancialInstrumentsEndpoint : EndpointWithoutRequest<List<string>>
    { 
        private readonly PriceService _priceService;

        public ListOfAvailableFinancialInstrumentsEndpoint(PriceService priceService)
        {
            _priceService = priceService;
        }

        public override void Configure()
        {
            Get("/api/ListOfAvailableFinancialInstruments");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var price = _priceService.ListOfAvailableFinancialInstruments();
            await SendAsync(price);
        }
    }

    
}
