using Application.Services;
using FastEndpoints;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Endpoints
{
	public class GetPriceEndpoint : EndpointWithoutRequest<decimal>
	{
		private readonly PriceService _priceService;

		public GetPriceEndpoint(PriceService priceService)
		{
			_priceService = priceService;
		}

		public override void Configure()
		{
			Get("/api/prices/{instrument}");
			AllowAnonymous();
		}

		public override async Task HandleAsync(CancellationToken ct)
		{
			var price = _priceService.GetPriceAsync(Route<string>("instrument"));
			await SendAsync(price);
		}
	}
}
