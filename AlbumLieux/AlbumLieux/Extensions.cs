using System.Net.Http;
using System.Text;

namespace AlbumLieux
{
	public static class Extensions
	{
		public static StringContent ToStringContent(this string data)
		{
			return new StringContent(data, Encoding.UTF8, "application/json");
		}
	}
}
