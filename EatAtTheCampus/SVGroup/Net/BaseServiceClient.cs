using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Yarx.Net
{
	public abstract class BaseServiceClient
	{
		public String ContentType { get; set; }
		public WebHeaderCollection Headers {get; set;}

		internal BaseServiceClient()
		{
			Headers = new WebHeaderCollection ();
		}

		protected Task<string> MakeAsyncRequest(string url)
		{
			var request = (HttpWebRequest)WebRequest.Create(url);
			request.ContentType = ContentType;
			request.Headers = Headers;
			return Task.Factory.FromAsync(request.BeginGetResponse, asyncResult => request.EndGetResponse(asyncResult), null)
				.ContinueWith(t => this.ReadFromStreamResponse(t.Result));
		}

		protected string ReadFromStreamResponse(WebResponse response)
		{
			using (var responseStream = response.GetResponseStream())
			{
				using (var reader = new StreamReader(responseStream))
				{
					var content = reader.ReadToEnd();
					return content;
				}
			}
		}
	}
}

