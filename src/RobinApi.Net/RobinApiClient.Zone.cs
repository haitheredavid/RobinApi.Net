using RobinApi.Net.Exceptions;
using RobinApi.Net.Helpers;
using RobinApi.Net.Model;
using RobinApi.Net.Wrappers;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RobinApi.Net
{

  public partial class RobinApiClient
  {


    public async Task<Zone> GetZone(int id)
    {
      return await Get<Zone>($"zones/{id}").ConfigureAwait(false);
    }

    public async Task<Seat[]> GetZoneSeat(int id, KeyValuePair<string, string>[] query = null)
    {
      return await Get<Seat[]>($"zones/{id}/seats/", query).ConfigureAwait(false);
    }
  }

}
