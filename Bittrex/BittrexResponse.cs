using System;
using Newtonsoft.Json;

namespace Bittrex
{
	public class BittrexResponse<T> : BittrexResponse
	{
		[JsonProperty("result")]
		public T Result { get; set; }
	}

	public class BittrexResponse
	{
		[JsonProperty("success")]
		public Boolean Success { get; set; }

		[JsonProperty("message")]
		public String Message { get; set; }
	}
}
