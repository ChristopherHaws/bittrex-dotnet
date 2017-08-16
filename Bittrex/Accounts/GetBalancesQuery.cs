using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bittrex
{
	public static class GetBalancesQuery
	{
		public static async Task<Balance[]> GetBalancesAsync(this BittrexClient client)
		{
			var request = new BittrexRequest
			{
				Api = BittrexApi.Account,
				RelativeUrl = "api/v1.1/account/getbalances"
			};

			var response = await client.SendRequestAsync<Balance[]>(request);

			return response.Result;
		}

		public class Balance
		{
			[JsonProperty("Currency")]
			public String Currency { get; set; }

			[JsonProperty("Balance")]
			public Decimal Value { get; set; }

			[JsonProperty("Available")]
			public Decimal Available { get; set; }

			[JsonProperty("Pending")]
			public Decimal Pending { get; set; }

			[JsonProperty("CryptoAddress")]
			public String CryptoAddress { get; set; }

			[JsonProperty("Requested")]
			public Boolean Requested { get; set; }

			[JsonProperty("Uuid")]
			public String Uuid { get; set; }
		}
	}
}
