using System;

namespace Citas_API.Entities;

public class Appointment
{
    public int Id { get; set; }

    public required string DocIdType { get; set; }

    public required string NumDocId { get; set; }

    public required string FullName { get; set; }

    public int SpecialtyId { get; set; }

    public required Specialty SpecialtyAppointment { get; set; }

    public DateTimeOffset CreationDateTime { get; set; }
}
