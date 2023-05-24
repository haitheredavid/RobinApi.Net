using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobinApi.Net
{

  public class RobinQuery
  {
    const string DATE = "yyyy-MM-ddTHH:mm:sszzz";

    public static string Create(string url, KeyValuePair<string,string>[] query = null)
    {
      var urlBuilder = new StringBuilder(url);
      if(query != null && query.Any())
      {

        var queryString = string.Join("&",
          query.Select(
            p =>
              string.IsNullOrEmpty(p.Value)
                ? $"{Uri.EscapeDataString(p.Key)}="
                : $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));

        if(!string.IsNullOrWhiteSpace(queryString))
        {
          urlBuilder.Append("?" + queryString);
        }
      }
        
      return urlBuilder.ToString();
    }
      
    public static class Fields
    {
      public static KeyValuePair<string, string> After(DateTime? item) => new KeyValuePair<string, string>("after", item.Value.ToString(DATE));
      public static KeyValuePair<string, string> Before(DateTime? item) => new KeyValuePair<string, string>("before", item.Value.ToString(DATE));
      public static KeyValuePair<string, string> ZoneIds(string[] item) => new KeyValuePair<string, string>("zone_ids", string.Join(",", item));
      public static KeyValuePair<string, string> LocationIds(string[] item) => new KeyValuePair<string, string>("location_ids", string.Join(",", item));
      public static KeyValuePair<string, string> SpaceIds(string[] item) => new KeyValuePair<string, string>("space_ids", string.Join(",", item));
      public static KeyValuePair<string, string> SeatIds(string[] item) => new KeyValuePair<string, string>("seat_ids", string.Join(",", item));
      public static KeyValuePair<string, string> Page(int item) => new KeyValuePair<string, string>("page", item.ToString());
      public static KeyValuePair<string, string> PerPage(int item) => new KeyValuePair<string, string>("per_page", item.ToString());
    }
      
  }

}
