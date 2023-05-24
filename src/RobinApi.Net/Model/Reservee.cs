using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{

  [DataContract]
  public class Reservee
  {
    
    [DataMember(Name = "email", EmitDefaultValue = false)]
    public string Email { get; set; }

    [DataMember(Name = "user_id", EmitDefaultValue = false)]
    public int UserId { get; set; }

    [DataMember(Name = "visitor_id", EmitDefaultValue = false)]
    public int? VisitorId { get; set; }

    [DataMember(Name = "participation_status", EmitDefaultValue = false)]
    public string ParticipationStatus { get; set; }
  }

}
