using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{

  [DataContract]
  public class Reservation
  {
    [DataMember(Name = "id", EmitDefaultValue = false)]
    public string Id { get; set; }

    [DataMember(Name = "group_seat_reservation_id", EmitDefaultValue = false)]
    public int? GroupSeatReservationId { get; set; }

    [DataMember(Name = "seat_id", EmitDefaultValue = false)]
    public int SeatId { get; set; }

    [DataMember(Name = "reserver_id", EmitDefaultValue = false)]
    public string ReserverId { get; set; }

    [DataMember(Name = "type", EmitDefaultValue = false)]
    public string Type { get; set; }

    [DataMember(Name = "title", EmitDefaultValue = false)]
    public string Title { get; set; }

    [DataMember(Name = "start", EmitDefaultValue = false)]
    public DateTimeZone Start { get; set; }

    [DataMember(Name = "end", EmitDefaultValue = false)]
    public DateTimeZone End { get; set; }

    [DataMember(Name = "recurrence", EmitDefaultValue = false)]
    public string[] Recurrence { get; set; }

    [DataMember(Name = "series_id", EmitDefaultValue = false)]
    public string SeriesId { get; set; }

    [DataMember(Name = "recurrence_id", EmitDefaultValue = false)]
    public string RecurrenceId { get; set; }

    [DataMember(Name = "created_at", EmitDefaultValue = false)]
    public DateTime CreatedAt { get; set; }

    [DataMember(Name = "updated_at", EmitDefaultValue = false)]
    public DateTime UpdatedAt { get; set; }

    [DataMember(Name = "reservee", EmitDefaultValue = false)]
    public Reservee Reservee { get; set; }

    [DataMember(Name = "status", EmitDefaultValue = false)]
    public string Status { get; set; }
  }

}
