using Application.Services;
using Domain.Extension;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace WebSocket
{
	public class AmegaHub(PriceService PriceService) : Hub
	{
		public async Task SubscribeToPrice(string instrument)
		{
			var eventName = nameof(SubscribeToPrice);
			try
			{
				Console.WriteLine($"{eventName} started with instrument {instrument}");
				var Instruments = PriceService.ListOfAvailableFinancialInstruments();
				if (Instruments.Contains(instrument))
				{
					await Groups.AddToGroupAsync(Context.ConnectionId, instrument);
					await Clients.Caller.SendAsync("SubscriptionSuccess", instrument);
					Console.WriteLine($"{eventName} SubscriptionSuccess with instrument {instrument} and ConnectionId {Context.ConnectionId}");
				}
				else
				{
					await Clients.Caller.SendAsync("SubscriptionFailed", "Invalid instrument");
					Console.WriteLine($"{eventName} SubscriptionFailed Invalid instrument, with instrument {instrument} and ConnectionId {Context.ConnectionId}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{eventName} error: {ex.FullTextError()}");
			}
			
		}

		public async Task UnsubscribeFromPrice(string instrument)
		{
			var eventName = nameof(UnsubscribeFromPrice);
			try
			{
				Console.WriteLine($"{eventName} started with instrument {instrument}");
				var Instruments = PriceService.ListOfAvailableFinancialInstruments();
				if (Instruments.Contains(instrument))
				{
					await Groups.RemoveFromGroupAsync(Context.ConnectionId, instrument);
					await Clients.Caller.SendAsync("UnSubscriptionSuccess", instrument);
					Console.WriteLine($"{eventName} UnSubscriptionSuccess with instrument {instrument} and ConnectionId {Context.ConnectionId}");
				}
				else
				{
					await Clients.Caller.SendAsync("UnSubscriptionFailed", "Invalid instrument");
					Console.WriteLine($"{eventName} UnSubscriptionFailed Invalid instrument, with instrument {instrument} and ConnectionId {Context.ConnectionId}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{eventName} error: {ex.FullTextError()}");
			}	
		}

		public async Task BroadcastPrice(string instrument)
		{
			var eventName = nameof(BroadcastPrice);
			Console.WriteLine($"{eventName} started with instrument {instrument}");
			try
			{
				//Efficiently broadcast to all clients subscribed to the specific instrument group
				var price = PriceService.GetPriceAsync(instrument);
				await Clients.Group(instrument).SendAsync("ReceivePrice", instrument, price);
				Console.WriteLine($"{eventName} ReceivePrice with instrument {instrument} and price {price}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{eventName} error: {ex.FullTextError()}");
			}			
		}
	}
}


