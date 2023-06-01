using System.Runtime.Serialization;

namespace RobinApi.Net.Wrappers
{

  [DataContract]
  public class PagingWrapper
  {
    [DataMember(Name = "page", EmitDefaultValue = false)]
    public int Page { get; set; }

    [DataMember(Name = "per_page", EmitDefaultValue = false)]
    public int PerPage { get; set; }

    [DataMember(Name = "has_next_page", EmitDefaultValue = false)]
    public bool HasNextPage { get; set; }
  }

}
