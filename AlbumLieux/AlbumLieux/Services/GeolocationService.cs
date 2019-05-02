using System;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using System.Collections.Generic;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using System.Linq;

namespace AlbumLieux.Services
{
	public interface IGeolocationService
	{
		Task<Position> GetMyPosition();
	}

	public class GeolocationService : IGeolocationService
	{
		private async Task CheckPermissions()
		{
			var permissionToAsk = new List<Permission>();

			async Task AddIfNecessary(Permission perm)
			{
				var check = await CrossPermissions.Current.CheckPermissionStatusAsync(perm);
				if (check != PermissionStatus.Granted)
				{
					permissionToAsk.Add(perm);
				}
			}

			await AddIfNecessary(Permission.LocationWhenInUse);

			var results = await CrossPermissions.Current.RequestPermissionsAsync(permissionToAsk.ToArray());

			if (results.Any(x => x.Value != PermissionStatus.Granted))
			{
				throw new NotImplementedException();
			}
		}

		public async Task<Position> GetMyPosition()
		{
			if (!CrossGeolocator.IsSupported)
			{
				throw new NotSupportedException();
			}
			else
			{
				await CheckPermissions(); 
				try
				{
					var position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(5));
					return position;
				}
				catch (TaskCanceledException)
				{
					var lastPosition = await CrossGeolocator.Current.GetLastKnownLocationAsync();
					return lastPosition;
				}
			}
		}
	}
}
