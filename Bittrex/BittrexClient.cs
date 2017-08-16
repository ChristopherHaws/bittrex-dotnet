using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bittrex
{
	public class BittrexClient
	{
		private const String BaseUrl = "https://bittrex.com";

		private readonly String key;
		private readonly String secret;

		public BittrexClient(String key, String secret)
		{
			this.key = key;
			this.secret = secret;
		}

		public async Task<BittrexResponse<T>> SendRequestAsync<T>(BittrexRequest request)
		{
			using (var http = new HttpClient())
			{
				var requestMessage = request.Api == BittrexApi.Public
					? this.BuildUnauthenticatedRequestAsync(request)
					: this.BuildAuthenticatedRequestAsync(request);

				var response = await http.SendAsync(requestMessage);
				var content = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					var error = JsonConvert.DeserializeObject<BittrexResponse>(content);

					throw new BittrexException(error.Message);
				}

				var value = JsonConvert.DeserializeObject<BittrexResponse<T>>(content);
				if (!value.Success)
				{
					throw new BittrexException(value.Message);
				}

				return value;
			}
		}

		private HttpRequestMessage BuildUnauthenticatedRequestAsync(BittrexRequest request)
		{
			var builder = new UriBuilder(BaseUrl)
			{
				Path = request.RelativeUrl,
			};

			if (request.Parameters.Count > 0)
			{
				builder.Query = request.Parameters.ToQueryString();
			}

			var requestMessage = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
			requestMessage.Headers.Add("User-Agent", "bittrex-dotnet");

			return requestMessage;
		}

		private HttpRequestMessage BuildAuthenticatedRequestAsync(BittrexRequest request)
		{
			var query = new NameValueCollection
			{
				{ "apikey", this.key },
				{ "nonce", (DateTime.UtcNow.ToUnixTimestamp() * 100000).ToString() },
				request.Parameters
			}.ToQueryString();

			var builder = new UriBuilder(BaseUrl)
			{
				Path = request.RelativeUrl,
				Query = query
			};

			var requestMessage = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
			requestMessage.Headers.Add("User-Agent", "bittrex-dotnet");

			var hash = this.HashString(builder.Uri.ToString(), this.secret);
			requestMessage.Headers.Add("apisign", hash);

			return requestMessage;
		}

		private String HashString(String value, String secret)
		{
			var valueBytes = Encoding.UTF8.GetBytes(value);
			var secretBytes = Encoding.UTF8.GetBytes(secret);

			using (var hmac = new HMACSHA512(secretBytes))
			{
				var hash = hmac.ComputeHash(valueBytes);

				return hash.ToHexString();
			}
		}
	}
}
