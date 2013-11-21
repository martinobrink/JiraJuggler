using System;
using System.Net.Http;

namespace JiraJuggler.Shared
{
	public class SharedHttpClient
	{
		private HttpClient client = new HttpClient();

		public string PerformRequest()
		{
			return client.GetStringAsync("http://www.google.dk").Result;
		}
	}
}
