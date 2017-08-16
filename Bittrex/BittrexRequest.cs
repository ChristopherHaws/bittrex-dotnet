using System;
using System.Collections.Specialized;

namespace Bittrex
{
	public class BittrexRequest
	{
		public BittrexApi Api { get; set; }
		public String RelativeUrl { get; set; }
		public NameValueCollection Parameters { get; set; } = new NameValueCollection();
	}
}
