using System;
using System.Runtime.Serialization;

namespace AlbumLieux.Exceptions
{
	public class EmptyFieldException : Exception
	{
		public string FieldName { get; }

		public EmptyFieldException(string fieldName)
		{
			FieldName = fieldName;
		}

		protected EmptyFieldException(SerializationInfo info, StreamingContext context) : base(info, context) { }

	}
}
