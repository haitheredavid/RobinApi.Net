using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{

  [DataContract]
  public class Seat
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    public int Id { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    public string Name { get; set; }
    
    [DataMember(Name = "space_id", EmitDefaultValue = false)]
    public int SpaceId { get; set; }

    [DataMember(Name = "zone_id", EmitDefaultValue = false)]
    public int ZoneId { get; set; }

    [DataMember(Name = "is_disabled", EmitDefaultValue = false)]
    public bool IsDisabled { get; set; }
    
    [DataMember(Name = "is_reservable", EmitDefaultValue = false)]
    public bool IsReservable { get; set; }

    [DataMember(Name = "created_at", EmitDefaultValue = false)]
    public DateTime CreatedAt { get; set; }

    [DataMember(Name = "updated_at", EmitDefaultValue = false)]
    public DateTime UpdatedAt { get; set; }

    [DataMember(Name = "disabled_at", EmitDefaultValue = false)]
    public DateTime ?DisabledAt { get; set; }

  }

}
