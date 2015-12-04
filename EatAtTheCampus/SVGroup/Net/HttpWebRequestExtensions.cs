using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Yarx.Net
{

    public static class HttpWebRequestExtensions
    {
        public static Task<T> CreateWebRequestTask<T>(this HttpWebRequest request, Func<byte[]> dataGetter)
        {
			var task = CreateWebRequestTaskEx(request, dataGetter);
            task.Wait();
            return Task<T>.Factory.StartNew(() => DeserializeObjectFromResult<T>(task));
        }
			
        private static T DeserializeObjectFromResult<T>(Task<byte[]> task)
        {
            var buffer = Encoding.Convert(Encoding.GetEncoding("iso-8859-1"), Encoding.UTF8, task.Result);
            var tempString = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(tempString);
        }

		public static Task<byte[]> CreateWebRequestTaskEx(HttpWebRequest request, Func<byte[]> dataGetter)
        {
            return Task<byte[]>.Factory.StartNew(
                () =>
                    {
                        //Exception exception = null;
                        byte[] responseData = null;

                        var getRequestStreamTask =
                            Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, 
                                                           request.EndGetRequestStream, 
                                                           null)
                                                        .ContinueWith(task => WriteDataToOutStream(dataGetter, task));

                        getRequestStreamTask.Wait();
                        
                        Task requestTask =
                            Task.Factory.FromAsync<WebResponse>(
                                request.BeginGetResponse,
                                request.EndGetResponse,
                                null,
                                TaskCreationOptions.AttachedToParent)
                                .ContinueWith(task => responseData = GetData(task.Result));

                        requestTask.Wait();

                        return responseData;
                    },
                TaskCreationOptions.AttachedToParent);
        }

        private static void WriteDataToOutStream(Func<byte[]> dataGetter, Task<Stream> task)
        {
            var outStream = task.Result;
            var data = dataGetter();
            if (data == null || data.Length == 0)
            {
                return;
            }

            outStream.Write(data, 0, data.Length);
        }

        private static byte[] GetData(WebResponse response)
        {
            using (response)
            {
                var inStream = response.GetResponseStream();

                if (inStream != null)
                {
                    using (inStream)
                    {
                        return GetResponseData(inStream, response.ContentLength > 0 ? response.ContentLength : 1024);
                    }
                }
            }

            return null;
        }

        private static byte[] GetResponseData(Stream inStream, long bufferSize)
        {
            using (var output = new MemoryStream())
            {
                int count;
                do
                {
                    var buf = new byte[bufferSize];
                    count = inStream.Read(buf, 0, Convert.ToInt32(bufferSize));
                    output.Write(buf, 0, count);
                }
                while (inStream.CanRead && count > 0);
                return output.ToArray();
            }
        }
    }
}