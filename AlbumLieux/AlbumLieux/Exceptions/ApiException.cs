using System;
using System.Runtime.Serialization;

namespace AlbumLieux.Exceptions
{
	public class ApiException : Exception
	{
		public string ErrorCode { get; set; }
		public string ErrorMessage { get; set; }

		public ApiException(string errorCode, string errorMessage) : base(errorMessage)
		{
			ErrorCode = errorCode;
			if (string.IsNullOrEmpty(errorMessage))
			{
				ErrorMessage = $"ApiErrorCode : {errorCode}";
			}
			else
			{
				ErrorMessage = errorMessage;
			}
		}

		protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
