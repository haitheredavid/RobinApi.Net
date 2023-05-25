using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{

  [DataContract]
  public class Zone
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    public int Id { get; set; }

    [DataMember(Name = "space_id", EmitDefaultValue = false)]
    public int SpaceId { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    public string Name { get; set; }
    
    [DataMember(Name = "type", EmitDefaultValue = false)]
    public string Type { get; set; }

    [DataMember(Name = "created_at", EmitDefaultValue = false)]
    public DateTime CreatedAt { get; set; }

    [DataMember(Name = "updated_at", EmitDefaultValue = false)]
    public DateTime UpdatedAt { get; set; }

  }

}
