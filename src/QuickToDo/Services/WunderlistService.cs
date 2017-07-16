using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickToDo.Services.Dto;

namespace QuickToDo.Services
{
  public class WunderlistService : ITaskService
  {
    private readonly HttpClient _httpClient;

    public WunderlistService(string accessToken, string clientId)
    {
      var proxy = WebRequest.DefaultWebProxy;
      proxy.Credentials = CredentialCache.DefaultNetworkCredentials;

      var httpClientHandler = new HttpClientHandler() { Proxy = proxy };

      _httpClient = new HttpClient(httpClientHandler)
      {
        BaseAddress = new Uri(ConfigurationManager.AppSettings["WunderlistApiUri"])
      };
      
      _httpClient.DefaultRequestHeaders.Accept.Clear();
      _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      _httpClient.DefaultRequestHeaders.Add("X-Access-Token", accessToken);
      _httpClient.DefaultRequestHeaders.Add("X-Client-ID", clientId);
    }

    private void CreateTask(int listId, string title)
    {
      var param = JsonConvert.SerializeObject(new { list_id = listId, title = title });

      HttpContent content = new StringContent(param, Encoding.UTF8, "application/json");
      var response = _httpClient.PostAsync("tasks", content).Result;

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception(response.StatusCode.ToString());
      }
    }

    private int GetListId(string name)
    {
      var result = 0;

      var response = _httpClient.GetAsync("lists").Result;

      if (response.IsSuccessStatusCode)
      {
        var responseString = response.Content.ReadAsStringAsync().Result;

        var wunderlistLists = JsonConvert.DeserializeObject<List<WunderlistList>>(responseString);

        result = wunderlistLists.First(l => l.Title == name).Id;
      }

      return result;
    }

    public async Task Create(string title)
    {
      var indoxId = GetListId("inbox");

      CreateTask(indoxId, title);
    }
  }
}
