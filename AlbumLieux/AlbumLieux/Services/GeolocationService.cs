using System;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using System.Collections.Generic;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using System.Linq;
using AlbumLieux.Exceptions;

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
				throw new MissingPermissionException(results.First(x => x.Value != PermissionStatus.Granted).Key.ToString());
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
					return await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(5));
				}
				catch (GeolocationException)
				{
					return await CrossGeolocator.Current.GetLastKnownLocationAsync();
				}
				catch (TaskCanceledException)
				{
					return await CrossGeolocator.Current.GetLastKnownLocationAsync();
				}
			}
		}
	}
}
