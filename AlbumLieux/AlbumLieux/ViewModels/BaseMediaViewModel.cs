using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Storm.Mvvm;

namespace AlbumLieux.ViewModels
{
	public class BaseMediaViewModel : ViewModelBase
	{
		private bool _isInitialized;

		private async Task CheckPermissions(Permission permission)
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

			await AddIfNecessary(permission);
			await AddIfNecessary(Permission.Storage);

			var results = await CrossPermissions.Current.RequestPermissionsAsync(permissionToAsk.ToArray());

			if (results.Any(x => x.Value != PermissionStatus.Granted))
			{
				throw new NotImplementedException();
			}
		}

		protected async Task<MediaFile> PickFromGallery()
		{
			if (!_isInitialized)
			{
				_isInitialized = await CrossMedia.Current.Initialize();
			}

			if (!CrossMedia.IsSupported || !CrossMedia.Current.IsPickPhotoSupported)
			{
				throw new NotImplementedException();
			}

			await CheckPermissions(Permission.Photos);

			return await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                CompressionQuality = 85
            });
		}

		protected async Task<MediaFile> PickFromCamera()
		{
			if (!_isInitialized)
			{
				_isInitialized = await CrossMedia.Current.Initialize();
			}

			if (!CrossMedia.IsSupported || !CrossMedia.Current.IsPickPhotoSupported || !CrossMedia.Current.IsCameraAvailable)
			{
				throw new NotImplementedException();
			}

			await CheckPermissions(Permission.Camera);

			return await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
			{
				CompressionQuality = 85
			});
		}
	}
}