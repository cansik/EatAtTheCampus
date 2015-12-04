using System;
using System.Threading.Tasks;
using System.Net;
using System.Text;

namespace Yarx.Net
{
	public class SimpleWebClient : BaseServiceClient
	{

		public SimpleWebClient ()
		{
		}

		public async Task<string> GetStringAsync(string url)
		{
			string response = await this.MakeAsyncRequest(url);
			var result = response;
			return result;
		}

		public async Task<string> PostStringAsync(string url, string postData)
		{
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.Headers = Headers;
			request.ContentType = ContentType;

			var task = HttpWebRequestExtensions.CreateWebRequestTaskEx (request, () => Encoding.UTF8.GetBytes (postData));
			var data = await task;
			var returnString = Encoding.UTF8.GetString(data, 0, data.Length);
			return returnString;
		}
	}
}

