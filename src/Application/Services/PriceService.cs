using Domain.Interfaces;
using System.Collections.Generic;

namespace Application.Services
{
	public class PriceService(IPriceProvider priceProvider)
	{
		private readonly IPriceProvider _priceProvider = priceProvider;

		public decimal GetPriceAsync(string instrument)
		{
			return _priceProvider.GetCurrentPrice(instrument);
		}

		public List<string> ListOfAvailableFinancialInstruments()
		{
			return _priceProvider.ListOfAvailableFinancialInstruments();
		}
	}
}
