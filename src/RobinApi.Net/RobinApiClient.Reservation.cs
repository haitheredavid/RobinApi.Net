using RobinApi.Net.Exceptions;
using RobinApi.Net.Helpers;
using RobinApi.Net.Model;
using RobinApi.Net.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinApi.Net
{

  public partial class RobinApiClient
  {

    public async Task<Reservation[]> GetReservations(KeyValuePair<string, string>[] query = null)
    {
      return await Get<Reservation[]>("reservations/seats", query);
    }


  }

}
