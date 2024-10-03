namespace Citas_API.Dtos;

public record class AppointmentDTO(
    string DocIdType,
    string NumDocId,
    string FullName,
    string Specialty,
    DateTimeOffset CreationDateTime
);
