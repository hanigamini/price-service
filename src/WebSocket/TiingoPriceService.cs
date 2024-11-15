using System;
using Websocket.Client;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using Domain.Extension;


namespace WebSocket
{
	public class TiingoPriceService
	{
		private readonly Uri url = new Uri("wss://stream.binance.com:443/ws/btcusdt");
		private readonly IHubContext<AmegaHub> _hubContext;

		public TiingoPriceService(IHubContext<AmegaHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public void Start()
		{
			try
			{
				var factory = new WebsocketClient(url);

				factory.MessageReceived.Subscribe(async message =>
				{
					Console.WriteLine(message.Text);
					var data = JObject.Parse(message.Text);
					string price = "";
					try
					{
						price = data["p"].ToString();
					}
					catch { }
					if (!string.IsNullOrEmpty(price))
					{
						//Efficiently manage 1,000+ WebSocket subscribers with SignalR Backplanes such as Redis or SQL Server
						//Or Load Balancing, distribute webSocket connections across multiple servers using a load balancer.
						// Broadcasting the message to a group of clients subscribed to this instrument
						await _hubContext.Clients.Group("BTCUSD").SendAsync("ReceivePrice", "BTCUSD", price);
					}
				});

				factory.Start().Wait();

				var subscriptionMessage = "{\"method\": \"SUBSCRIBE\",\"params\": [\"btcusdt@aggTrade\"],\"id\": 1}";
				factory.Send(subscriptionMessage);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"TiingoPriceService start error: {ex.FullTextError()}");
			}
		}
	}


}
