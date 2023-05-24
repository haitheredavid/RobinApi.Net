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
    /// Get a list of supported device manifests
    /// </summary>
    /// <param name="page">The page to return</param>
    /// <param name="perPage">The amount of results to return per page</param>
    /// <returns></returns>
    public async Task<DeviceManifest[]> GetDeviceManifests(int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("device-manifests");
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<DeviceManifest[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get details about a devicemanifest
    /// </summary>
    /// <param name="id">The ID or slug of the devicemanifest</param>
    /// <returns></returns>
    public async Task<DeviceManifest> GetDeviceManifest(string id)
    {
      var urlBuilder = new StringBuilder("device-manifests/" + id);
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<DeviceManifest>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Returns a Device resource, containing information about the device.
    /// </summary>
    /// <param name="id">The ID of the device</param>
    /// <returns>Returns an Device resource</returns>
    public async Task<Device> GetDevice(int id)
    {
      var urlBuilder = new StringBuilder("devices/" + id);
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Device>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Create a new device.
    /// </summary>
    /// <param name="device"></param>
    /// <returns></returns>
    public async Task<Device> AddDevice(Device device)
    {
      var urlBuilder = new StringBuilder("me/devices");
      var content = new StringContent(JsonHelper.Serialize(device), Encoding.UTF8, "application/json");
      var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Device>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Add identifier to a device
    /// </summary>
    /// <param name="id">The ID of the device to post identifier to.</param>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public async Task<Identifier> AddDeviceIdentifier(int id, Identifier identifier)
    {
      var urlBuilder = new StringBuilder("devices/" + id + "/identifiers");
      var content = new StringContent(JsonHelper.Serialize(identifier), Encoding.UTF8, "application/json");
      var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Identifier>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get all of a device's spaces
    /// </summary>
    /// <param name="id">The ID of the device</param>
    /// <param name="query">Will filter by a specified space name</param>
    /// <param name="page">The page of the result</param>
    /// <param name="perPage">How many results are returned per page</param>
    /// <returns></returns>
    public async Task<Space[]> GetDeviceSpaces(int id, string query = null, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("devices/" + id + "/spaces");
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
    /// Permanently deletes a Device resource and all of it's related submodels.
    /// </summary>
    /// <param name="id">The ID of the device to remove</param>
    /// <returns></returns>
    public async Task DeleteDevice(int id)
    {
      var urlBuilder = new StringBuilder("devices/" + id);
      var response = await _httpClient.DeleteAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }
  }

}
