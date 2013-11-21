using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JiraJuggler.Shared
{
	public class JiraClient
	{
		private readonly HttpClient _client = new HttpClient();

		public string PerformRequest()
		{
			return _client.GetStringAsync("http://www.google.dk").Result;
		}

        async public Task<HttpResponseMessage> UploadImage(string url, byte[] imageData, string fileName)
        {
            var requestContent = new MultipartFormDataContent();
            //    here you can specify boundary if you need---^
            var imageContent = new ByteArrayContent(imageData);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            requestContent.Add(imageContent, "file", fileName);

            return await _client.PostAsync(url, requestContent);
        }
	}
}
