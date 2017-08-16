using System;

namespace Bittrex
{
	[Serializable]
	internal class BittrexException : Exception
	{
		public BittrexException()
		{
		}

		public BittrexException(String message)
			: base(message)
		{
		}

		public BittrexException(String message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
