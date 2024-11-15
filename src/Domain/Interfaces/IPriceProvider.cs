using System.Collections.Generic;

namespace Domain.Interfaces
{
	public interface IPriceProvider
	{
		decimal GetCurrentPrice(string instrument);

		List<string> ListOfAvailableFinancialInstruments();
	}
}
