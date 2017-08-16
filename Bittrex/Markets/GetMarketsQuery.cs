using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bittrex
{
	public static class GetMarketsQuery
	{
		public static async Task<Market[]> GetMarketsAsync(this BittrexClient client)
		{
			var request = new BittrexRequest
			{
				Api = BittrexApi.Public,
				RelativeUrl = "api/v1.1/public/getmarkets"
			};

			var response = await client.SendRequestAsync<Market[]>(request);

			return response.Result;
		}

		public class Market
		{
			[JsonProperty("MarketCurrency")]
			public String MarketCurrencySymbol { get; set; }

			[JsonProperty("BaseCurrency")]
			public String BaseCurrencySymbol { get; set; }

			[JsonProperty("MarketCurrencyLong")]
			public String MarketCurrencyName { get; set; }

			[JsonProperty("BaseCurrencyLong")]
			public String BaseCurrencyName { get; set; }

			[JsonProperty("MinTradeSize")]
			public Decimal MinTradeSize { get; set; }

			[JsonProperty("MarketName")]
			public String MarketName { get; set; }

			[JsonProperty("IsActive")]
			public Boolean IsActive { get; set; }

			[JsonProperty("Created")]
			public DateTime Created { get; set; }
		}
	}
}
