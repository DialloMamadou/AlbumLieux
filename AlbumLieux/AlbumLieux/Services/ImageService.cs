using AlbumLieux.Exceptions;
using AlbumLieux.Models;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AlbumLieux.Services
{
	public interface IImageService
	{
		Task<Image> UploadImage(Stream image);
	}

	public class ImageService : BaseDataService, IImageService
	{
		public async Task<Image> UploadImage(Stream image)
		{
			var requestContent = new MultipartFormDataContent();

			var imageContent = new StreamContent(image);
			imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

			requestContent.Add(imageContent, "file", "temp.jpg");

			var response = await PostAsync<Image>("/images", requestContent, authenticated: true);

			if (response.IsSuccess)
			{
				return response.Data;
			}
			else
			{
				throw new ApiException(response.ErrorCode, response.ErrorMessage);
			}
		}
	}
}
