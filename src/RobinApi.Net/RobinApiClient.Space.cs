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
    /// Create a new space in the location.
    /// </summary>
    /// <param name="id">The ID of the location</param>
    /// <param name="space"></param>
    /// <returns></returns>
    public async Task<Space> CreateSpace(int id, Space space)
    {
      var urlBuilder = new StringBuilder("locations/" + id + "/spaces");
      var content = new StringContent(JsonHelper.Serialize(space), Encoding.UTF8, "application/json");
      var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Space>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get details about a space
    /// </summary>
    /// <param name="id">The ID of the space to return</param>
    /// <param name="include">Optional submodel includes, such as calendar.</param>
    /// <returns></returns>
    public async Task<Space> GetSpace(int id, string[] include = null)
    {
      var urlBuilder = new StringBuilder("spaces/" + id);
      var parameters = new Dictionary<string, string>();
      if(include != null)
        parameters.Add("include", string.Join(",", include));
      urlBuilder.Append(GetQueryString(parameters));
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Space>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Update's the properties of an existing Space.
    /// </summary>
    /// <param name="space"></param>
    /// <returns></returns>
    public async Task UpdateSpace(Space space)
    {
      var urlBuilder = new StringBuilder("spaces/" + space.Id);
      var content = new StringContent(JsonHelper.Serialize(space), Encoding.UTF8, "application/json");
      var response = await _httpClient.PatchAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Permanently deletes a Space resource and all of it's related submodels, such as events.
    /// </summary>
    /// <param name="id">The ID of the space to remove</param>
    /// <returns></returns>
    public async Task DeleteSpace(int id)
    {
      var urlBuilder = new StringBuilder("spaces/" + id);
      var response = await _httpClient.DeleteAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get a list of a space's events
    /// </summary>
    /// <param name="id">The ID of the space</param>
    /// <param name="after">Lower bound for an event's end property</param>
    /// <param name="before">Upper bound for an event's start property</param>
    /// <param name="page">The page to return</param>
    /// <param name="perPage">The amount of results to return per page</param>
    /// <returns></returns>
    public async Task<Event[]> GetSpaceEvents(int id, DateTime? after = null, DateTime? before = null, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/events");
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

    /// <summary>
    /// Book an event in a space
    /// </summary>
    /// <param name="id">The ID of the space</param>
    /// <param name="event"></param>
    /// <returns></returns>
    public async Task<Event> AddSpaceEvent(int id, Event @event)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/events");
      var content = new StringContent(JsonHelper.Serialize(@event), Encoding.UTF8, "application/json");
      var response = await _httpClient.PostAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Event>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get the amenities for a space.
    /// </summary>
    /// <param name="id">The ID of the space</param>
    /// <returns></returns>
    public async Task<Amenity[]> GetSpaceAmenities(int id)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/amenities");
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Amenity[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get the amenities for a space.
    /// </summary>
    /// <param name="id">The ID of the space</param>
    /// <param name="amenityId">The amenity ID to add to the space</param>
    /// <returns></returns>
    public async Task<Amenity> GetSpaceAmenity(int id, int amenityId)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/amenities/" + amenityId);
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Amenity>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Adds an amenity to a space.
    /// </summary>
    /// <param name="id">The ID of the space</param>
    /// <param name="amenityId">The amenity ID to add to the space</param>
    /// <returns></returns>
    public async Task<Amenity> AddAmenityToSpace(int id, int amenityId)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/amenities/" + amenityId);
      var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
      var response = await _httpClient.PutAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Amenity>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Removes an amenity from a space.
    /// </summary>
    /// <param name="id">The ID of the space</param>
    /// <param name="amenityId">The amenity ID to remove from the space</param>
    /// <returns></returns>
    public async Task RemoveSpaceAmenity(int id, int amenityId)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/amenities/" + amenityId);
      var response = await _httpClient.DeleteAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Get any current presence for a space
    /// </summary>
    /// <param name="id">The ID of the space</param>
    /// <param name="query">Will filter by a specified space name</param>
    /// <param name="page">The page of the result</param>
    /// <param name="perPage">How many results are returned per page</param>
    /// <returns></returns>
    public async Task<Presence[]> GetSpacePresence(int id, string query = null, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/presence");
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
        return JsonHelper.Deserialize<ApiWrapper<Presence[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Add presence to a space
    /// </summary>
    /// <param name="id">The ID of the space to post presence to.</param>
    /// <param name="presence"></param>
    /// <returns></returns>
    public async Task<Presence> AddSpacePresence(int id, Presence presence)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/presence");
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
    /// <param name="id">The ID of the space to remove presence from.</param>
    /// <param name="presence"></param>
    /// <returns></returns>
    public async Task DeleteSpacePresence(int id, Presence presence)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/presence");
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
    /// Returns a list of devices that belong to the Space resource. This is indicative of hardware that is physically inside the space, such as a beacon, motion sensor, or projector.
    /// </summary>
    /// <param name="id">The ID of the space</param>
    /// <param name="manifest">When provided, results will be filtered by the specified device manifest</param>
    /// <param name="page">The page of the result</param>
    /// <param name="perPage">How many results are returned per page</param>
    /// <returns></returns>
    public async Task<Device[]> GetSpaceDevices(int id, string manifest = null, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/devices");
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
    /// Gets the availability state for the space.
    /// </summary>
    /// <param name="id">The ID of the space</param>
    /// <returns></returns>
    public async Task<SpaceState> GetSpaceState(int id)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/state");
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<SpaceState>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Gets the calendar for the space.
    /// </summary>
    /// <param name="id">The ID of the space</param>
    /// <returns></returns>
    public async Task<Calendar> GetSpaceCalendar(int id)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/calendar");
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Calendar>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<Calendar>>(jsonResult).Meta);
    }

    /// <summary>
    /// Add calendar to a space
    /// </summary>
    /// <param name="id">The ID of the space</param>
    /// <param name="calendar"></param>
    /// <returns></returns>
    public async Task<Calendar> AddSpaceCalendar(int id, Calendar calendar)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/calendar");
      var content = new StringContent(JsonHelper.Serialize(calendar), Encoding.UTF8, "application/json");
      var response = await _httpClient.PutAsync(urlBuilder.ToString(), content).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Calendar>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<Calendar>>(jsonResult).Meta);
    }

    public async Task<Seat[]> GetSpaceSeats(int id, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("spaces/" + id + "/seats");
      var parameters = new Dictionary<string, string>
      {
        {"page", page.ToString()},
        {"per_page", perPage.ToString()}
      };
      urlBuilder.Append(GetQueryString(parameters));
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if(response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<Seat[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }

    /// <summary>
    /// Search space availability based on multiple parameters and get back results in a "Best Fit" order
    /// </summary>
    /// <param name="locationIds">One or more location IDs. Will include all spaces in the given locations in the result.</param>
    /// <param name="spaceIds">One or more space IDs. Can be combined with location_ids</param>
    /// <param name="after">Lower bound for free-busy query.</param>
    /// <param name="before">Upper bound for free-busy query.</param>
    /// <param name="duration">The amount of time required between two events for a space to be considered "free".</param>
    /// <param name="types">One or more space types. Unmatched spaces will be filtered out entirely.</param>
    /// <param name="amenityIds">One or more amenity IDs. Unmatched spaces will be filtered out entirely.</param>
    /// <param name="query">Space name filter. Unmatched spaces will be filtered out entirely.</param>
    /// <param name="minCapacity">Filter for min space capacity. Unmatched spaces will be filtered out entirely.</param>
    /// <param name="maxCapacity">Filter for max space capacity. Unmatched spaces will be filtered out entirely.</param>
    /// <param name="page">The page to return</param>
    /// <param name="perPage">The amount of results to return per page</param>
    /// <returns></returns>
    public async Task<FreeBusy[]> GetFreeBusySpaces(int[] locationIds = null, int[] spaceIds = null, DateTime? after = null, DateTime? before = null, int duration = 30, string[] types = null, int[] amenityIds = null, string query = null, int? minCapacity = null, int? maxCapacity = null, int page = 1, int perPage = 10)
    {
      var urlBuilder = new StringBuilder("free-busy/spaces");
      var parameters = new Dictionary<string, string>();
      if (locationIds != null)
        parameters.Add("location_ids", string.Join(",", locationIds));
      if (spaceIds != null)
        parameters.Add("space_ids", string.Join(",", spaceIds));
      if (after.HasValue)
        parameters.Add("after", after.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
      if (before.HasValue)
        parameters.Add("before", before.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
      parameters.Add("duration", duration.ToString());
      if (types != null)
        parameters.Add("types", string.Join(",", types));
      if (amenityIds != null)
        parameters.Add("amenity_ids", string.Join(",", amenityIds));
      parameters.Add("query", query);
      if (minCapacity.HasValue)
        parameters.Add("min_capacity", minCapacity.Value.ToString());
      if (maxCapacity.HasValue)
        parameters.Add("max_capacity", maxCapacity.Value.ToString());
      parameters.Add("page", page.ToString());
      parameters.Add("per_page", perPage.ToString());
      urlBuilder.Append(GetQueryString(parameters));
      var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
      var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if (response.IsSuccessStatusCode)
      {
        return JsonHelper.Deserialize<ApiWrapper<FreeBusy[]>>(jsonResult).Data;
      }
      throw new RobinApiException(JsonHelper.Deserialize<ApiWrapper<object>>(jsonResult).Meta);
    }
  }

}
