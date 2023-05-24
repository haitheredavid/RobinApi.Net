using RobinApi.Net.Exceptions;
using RobinApi.Net.Helpers;
using RobinApi.Net.Model;
using RobinApi.Net.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RobinApi.Net
{

  public partial class RobinApiClient
  {
    /// <summary>
    /// Gets a User resource.
    /// </summary>
    /// <param name="id">The ID or slug of the user</param>
    /// <returns></returns>
    public async Task<User> GetUser(string id)
    {
      var urlBuilder = new StringBuilder("users/" + id);
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<User>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Gets a list of a User's presence history.
    /// </summary>
    /// <param name="id">The ID or slug of the user</param>
    /// <returns></returns>
    public async Task<Presence[]> GetUserPresence(string id)
    {
      var urlBuilder = new StringBuilder("users/" + id + "/presence");
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Presence[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get a user's events
    /// </summary>
    /// <param name="id">The ID or slug of the user</param>
    /// <param name="after">Lower bound for an event's end property</param>
    /// <param name="before">Upper bound for an event's start property</param>
    /// <param name="page">The page to fetch</param>
    /// <param name="perPage">The amount of results to return on a single page.</param>
    /// <returns></returns>
    public async Task<Event[]> GetUserEvents(string id, DateTime? after = null, DateTime? before = null, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("users/" + id + "/events");
      var parameters = new Dictionary<string, string>();
      if(after.HasValue)
        parameters.Add("after", after.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
      if(before.HasValue)
        parameters.Add("before", before.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
      parameters.Add("page", page.ToString());
      parameters.Add("per_page", page.ToString());
      urlBuilder.Append(GetQueryString(parameters));
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Event[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get the current authenticated user.
    /// </summary>
    /// <returns></returns>
    public async Task<User> GetMe()
    {
      var urlBuilder = new StringBuilder("me");
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<User>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get the currently authenticated user's presence.
    /// </summary>
    /// <returns></returns>
    public async Task<Presence[]> GetMyPresence()
    {
      var urlBuilder = new StringBuilder("me/presence");

      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Presence[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get a user's events
    /// </summary>
    /// <param name="after">Lower bound for an event's end property</param>
    /// <param name="before">Upper bound for an event's start property</param>
    /// <param name="page">The page to fetch</param>
    /// <param name="perPage">The amount of results to return on a single page.</param>
    /// <returns></returns>
    public async Task<Event[]> GetMyEvents(DateTime? after = null, DateTime? before = null, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("me/events");
      var parameters = new Dictionary<string, string>();
      if(after.HasValue)
        parameters.Add("after", after.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
      if(before.HasValue)
        parameters.Add("before", before.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
      parameters.Add("page", page.ToString());
      parameters.Add("per_page", page.ToString());
      urlBuilder.Append(GetQueryString(parameters));
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Event[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get the currently authenticated user's organizations.
    /// </summary>
    /// <returns></returns>
    public async Task<Organization[]> GetMyOrganizations()
    {
      var urlBuilder = new StringBuilder("me/organizations");

      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Organization[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }
  }

}
