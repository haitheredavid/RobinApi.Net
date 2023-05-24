using RobinApi.Net.Exceptions;
using RobinApi.Net.Extensions;
using RobinApi.Net.Helpers;
using RobinApi.Net.Model;
using RobinApi.Net.Wrappers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RobinApi.Net
{

  public partial class RobinApiClient
  {
    /// <summary>
    /// Creates a new location for the organization.
    /// </summary>
    /// <param name="id">The ID or slug of the organization</param>
    /// <param name="location"></param>
    /// <returns></returns>
    public async Task<Location> CreateLocation(string id, Location location)
    {
      var urlBuilder = new StringBuilder("organizations/" + id + "/locations");
      var content = new StringContent(JsonHelper.Serialize(location), Encoding.UTF8, "application/json");
      var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Location>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Returns a Location resource, containing information about the location.
    /// </summary>
    /// <param name="id">The ID of the location</param>
    /// <returns>Returns an Location resource</returns>
    public async Task<Location> GetLocation(int id)
    {
      var urlBuilder = new StringBuilder("locations/" + id);
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Location>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Updates a Location resource.
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public async Task UpdateLocation(Location location)
    {
      var urlBuilder = new StringBuilder("locations/" + location.Id);
      var content = new StringContent(JsonHelper.Serialize(location), Encoding.UTF8, "application/json");
      var response = await _httpClient.PatchAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get all of a location's spaces
    /// </summary>
    /// <param name="id">The ID of the location</param>
    /// <param name="query">Will filter by a specified space name</param>
    /// <param name="page">The page of the result</param>
    /// <param name="perPage">How many results are returned per page</param>
    /// <returns></returns>
    public async Task<Space[]> GetLocationSpaces(int id, string query = null, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("locations/" + id + "/spaces");
      var parameters = new Dictionary<string, string>
      {
        {"query", query},
        {"page", page.ToString()},
        {"per_page", perPage.ToString()}
      };
      urlBuilder.Append(GetQueryString(parameters));
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Space[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get current presence for a location
    /// </summary>
    /// <param name="id">The ID of the location</param>
    /// <param name="query">Will filter by a specified space name</param>
    /// <param name="page">The page of the result</param>
    /// <param name="perPage">How many results are returned per page</param>
    /// <param name="spaceIds">A list of space IDs to filter by.</param>
    /// <returns></returns>
    public async Task<Presence[]> GetLocationPresence(int id, string query = null, int page = 1, int perPage = 10, int[] spaceIds = null)
    {
      var urlBuilder = new StringBuilder("locations/" + id + "/presence");
      var parameters = new Dictionary<string, string>
      {
        {"query", query},
        {"page", page.ToString()},
        {"per_page", perPage.ToString()}
      };
      if(spaceIds != null)
        parameters.Add("spaceIds", string.Join(",", spaceIds));
      urlBuilder.Append(GetQueryString(parameters));
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Presence[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Add presence to a space
    /// </summary>
    /// <param name="id">The ID of the location to post presence to.</param>
    /// <param name="presence"></param>
    /// <returns></returns>
    public async Task<Presence> AddLocationPresence(int id, Presence presence)
    {
      var urlBuilder = new StringBuilder("locations/" + id + "/presence");
      var content = new StringContent(JsonHelper.Serialize(presence), Encoding.UTF8, "application/json");
      var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Presence>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Expire a user or device's presence in a space
    /// </summary>
    /// <param name="id">The ID of the location to remove presence from.</param>
    /// <param name="presence"></param>
    /// <returns></returns>
    public async Task DeleteLocationPresence(int id, Presence presence)
    {
      var urlBuilder = new StringBuilder("locations/" + id + "/presence");
      var content = new StringContent(JsonHelper.Serialize(presence), Encoding.UTF8, "application/json");
      var response = await _httpClient.DeleteAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Returns a list of devices that belong to the Location resource. This is indicative of hardware that is physically inside the location, such as a beacon, motion sensor, or projector.
    /// </summary>
    /// <param name="id">The ID of the location</param>
    /// <param name="manifest">When provided, results will be filtered by the specified device manifest</param>
    /// <param name="page">The page of the result</param>
    /// <param name="perPage">How many results are returned per page</param>
    /// <returns></returns>
    public async Task<Device[]> GetLocationDevices(int id, string manifest = null, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("locations/" + id + "/devices");
      var parameters = new Dictionary<string, string>();
      if(!string.IsNullOrEmpty(manifest))
        parameters.Add("manifest", manifest);
      parameters.Add("page", page.ToString());
      parameters.Add("per_page", perPage.ToString());
      urlBuilder.Append(GetQueryString(parameters));
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Device[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get a list of a location's events
    /// </summary>
    /// <param name="id">The ID of the location</param>
    /// <param name="after">Lower bound for an event's end property</param>
    /// <param name="before">Upper bound for an event's start property</param>
    /// <param name="page">The page to return</param>
    /// <param name="perPage">The amount of results to return per page</param>
    /// <returns></returns>
    public async Task<Event[]> GetLocationEvents(int id, DateTime? after = null, DateTime? before = null, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("locations/" + id + "/events");
      var parameters = new Dictionary<string, string>();
      if(after.HasValue)
        parameters.Add("after", after.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
      if(before.HasValue)
        parameters.Add("before", before.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
      parameters.Add("page", page.ToString());
      parameters.Add("per_page", perPage.ToString());
      urlBuilder.Append(GetQueryString(parameters));
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Event[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }
  }

}
