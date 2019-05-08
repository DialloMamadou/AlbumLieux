using System;
using System.Runtime.Serialization;

namespace AlbumLieux.Exceptions
{
	public class MissingPermissionException : Exception
	{

		public string PermissionName { get; set; }

		public MissingPermissionException(string permissionName) : base($"Missing {permissionName}")
		{
			PermissionName = permissionName;
		}

		protected MissingPermissionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
