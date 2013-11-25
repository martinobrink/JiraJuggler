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

        public async Task<HttpResponseMessage> UploadImage(string issueId, byte[] imageData, string fileName)
        {
            var imageContent = new ByteArrayContent(imageData);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            var requestContent = new MultipartFormDataContent {{imageContent, "file", fileName}};
            var request = new HttpRequestMessage(HttpMethod.Post, "issue/"+issueId+"/attachments") { Content = requestContent };
            request.Headers.Add("X-Atlassian-Token", "nocheck");//required in order to disable XSRF check
            
            return await _client.SendAsync(request);
        }
	}
}
