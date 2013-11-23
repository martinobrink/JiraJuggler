using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JiraJuggler.Shared.Model;
using Newtonsoft.Json;

namespace JiraJuggler.Shared
{
	public class JiraClient
	{
	    private readonly HttpClient _client;

	    public JiraClient(string jiraBaseUrl, string userName, string password)
	    {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:{1}", userName, password))));
	        _client.BaseAddress = new Uri(jiraBaseUrl + "/rest/api/2/");
	    }

        public async Task<List<ProjectData>> GetProjects()
        {
            var projectsJson = await _client.GetStringAsync("project");
            if (String.IsNullOrEmpty(projectsJson))
            {
                return new List<ProjectData> {new ProjectData {Name = "No Data Received..."}};
            }

            return await JsonConvert.DeserializeObjectAsync<List<ProjectData>>(projectsJson);
        }

        //TODO: not tested yet
        public async Task<HttpResponseMessage> UploadImage(string url, byte[] imageData, string fileName)
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
