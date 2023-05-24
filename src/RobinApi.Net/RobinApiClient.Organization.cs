using RobinApi.Net.Exceptions;
using RobinApi.Net.Helpers;
using RobinApi.Net.Model;
using RobinApi.Net.Wrappers;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RobinApi.Net
{

  public partial class RobinApiClient
  {


    /// <summary>
    /// Get an organization's details
    /// </summary>
    /// <param name="id">The ID or slug (i.e. username) of the organization</param>
    /// <returns>Returns an Organization resource</returns>
    public async Task<Organization> GetOrganization(string id)
    {
      var urlBuilder = new StringBuilder("organizations/" + id);
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Organization>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get all of an organization's locations
    /// </summary>
    /// <param name="id">The ID or slug of the organization</param>
    /// <param name="query">Will filter by a specified location name</param>
    /// <param name="page"></param>
    /// <param name="perPage"></param>
    /// <returns></returns>
    public async Task<Location[]> GetOrganizationLocations(string id, string query = null, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("organizations/" + id + "/locations");
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
        return JsonHelper.Deserialize<ApiWrapper<Location[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get all of users in an organization
    /// </summary>
    /// <param name="id">The ID or slug of the organization</param>
    /// <param name="query">Will filter by a specified user name</param>
    /// <param name="page">The page of the result</param>
    /// <param name="perPage">How many results are returned per page</param>
    /// <param name="ids">A list of IDs to retrieve</param>
    /// <returns></returns>
    public async Task<User[]> GetOrganizationUsers(string id, string query = null, int page = 1, int perPage = 10, int[] ids = null)
    {
      var urlBuilder = new StringBuilder("organizations/" + id + "/users");
      var parameters = new Dictionary<string, string>
      {
        {"query", query},
        {"page", page.ToString()},
        {"per_page", perPage.ToString()}
      };
      if(ids != null)
        parameters.Add("ids", string.Join(",", ids));
      urlBuilder.Append(GetQueryString(parameters));
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<User[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get details about an organization's user
    /// </summary>
    /// <param name="id">The ID or slug of the organization</param>
    /// <param name="userId">The ID or slug of the user</param>
    /// <returns></returns>
    public async Task<User> GetOrganizationUser(string id, string userId)
    {
      var urlBuilder = new StringBuilder("organizations/" + id + "/users/" + userId);
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<User>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get details about an organization's amenities
    /// </summary>
    /// <param name="id">The ID or slug of the organization</param>
    /// <returns></returns>
    public async Task<Amenity[]> GetOrganizationAmenities(string id)
    {
      var urlBuilder = new StringBuilder("organizations/" + id + "/amenities");
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Amenity[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Create a new amenity for the organization
    /// </summary>
    /// <param name="id">The ID or slug of the organization</param>
    /// <param name="amenity"></param>
    /// <returns></returns>
    public async Task<Amenity> AddOrganizationAmenity(string id, Amenity amenity)
    {
      var urlBuilder = new StringBuilder("organizations/" + id + "/amenities");
      var content = new StringContent(JsonHelper.Serialize(amenity), Encoding.UTF8, "application/json");
      var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Amenity>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Returns a list of devices that belong to the Organization resource. This is indicative of hardware that is physically inside the organization, such as a beacon, motion sensor, or projector.
    /// </summary>
    /// <param name="id">The ID or slug of the organization</param>
    /// <param name="manifest">When provided, results will be filtered by the specified device manifest</param>
    /// <param name="page">The page of the result</param>
    /// <param name="perPage">How many results are returned per page</param>
    /// <returns></returns>
    public async Task<Device[]> GetOrganizationDevices(int id, string manifest = null, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("organizations/" + id + "/devices");
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
    /// Invites one or more new users to an organization
    /// </summary>
    /// <param name="id">The ID or slug of the organization</param>
    /// <param name="users"></param>
    /// <returns></returns>
    public async Task<User[]> AddNewUserToOrganization(string id, User[] users)
    {
      var urlBuilder = new StringBuilder("organizations/" + id + "/users");
      var content = new StringContent(JsonHelper.Serialize(users), Encoding.UTF8, "application/json");
      var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if (response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<User[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }
  }

}
