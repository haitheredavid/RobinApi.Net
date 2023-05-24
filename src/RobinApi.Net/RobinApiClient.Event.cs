using RobinApi.Net.Exceptions;
using RobinApi.Net.Extensions;
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
    /// Get an event.
    /// </summary>
    /// <param name="id">The ID of the event</param>
    /// <param name="include">Optional submodel includes, such as confirmation and space.</param>
    /// <returns></returns>
    public async Task<Event> GetEvent(int id, string[] include = null)
    {
      var urlBuilder = new StringBuilder("events/" + id);
      var parameters = new Dictionary<string, string>();
      if(include != null)
        parameters.Add("include", string.Join(",", include));
      urlBuilder.Append(GetQueryString(parameters));
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Event>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Update's the properties of an existing Event.
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    public async Task UpdateEvent(Event @event)
    {
      var urlBuilder = new StringBuilder("events/" + @event.Id);
      var content = new StringContent(JsonHelper.Serialize(@event), Encoding.UTF8, "application/json");
      var response = await _httpClient.PatchAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Gets the confirmation or checks if a confirmation exists for an event.
    /// </summary>
    /// <param name="id">The ID of the event to get the confirmation for.</param>
    /// <returns></returns>
    public async Task<Confirmation> GetEventConfirmation(int id)
    {
      var urlBuilder = new StringBuilder("events/" + id + "/confirmation");
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Confirmation>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Confirms an event.
    /// </summary>
    /// <param name="id">The ID of the event to get the confirmation for.</param>
    /// <param name="confirmation"></param>
    /// <returns></returns>
    public async Task<Confirmation> ConfirmEvent(int id, Confirmation confirmation)
    {
      var urlBuilder = new StringBuilder("events/" + id + "/confirmation");
      var content = new StringContent(JsonHelper.Serialize(confirmation), Encoding.UTF8, "application/json");
      var response = await _httpClient.PutAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Confirmation>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Removes a confirmation for an event.
    /// </summary>
    /// <param name="id">The ID of the event to remove the confirmation for.</param>
    /// <returns></returns>
    public async Task RemoveEventConfirmation(int id)
    {
      var urlBuilder = new StringBuilder("events/" + id + "/confirmation");
      var response = await _httpClient.DeleteAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Cancels an event.
    /// </summary>
    /// <param name="id">The ID of the event to delete.</param>
    /// <returns></returns>
    public async Task DeleteEvent(int id)
    {
      var urlBuilder = new StringBuilder("events/" + id);
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
