using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class PriceProvider : IPriceProvider
    {

		public decimal GetCurrentPrice(string instrument)
		{
			var prices = new Dictionary<string, decimal> { { "EURUSD", 2.45m }, { "USDJPY", 250.23m }, { "BTCUSD", 2562.78m } };
			if (prices.ContainsKey(instrument))
			{
				return prices[instrument];
			}
			else
				return 0;
		}

		public List<string> ListOfAvailableFinancialInstruments()
		{
			List<string> financialInstruments = ["EURUSD", "USDJPY", "BTCUSD"];
			return financialInstruments;
		}
	}

    
}
