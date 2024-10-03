namespace Citas_API.Dtos;

public record class CreatedAppointmentDTO(
    string DocIdType,
    string NumDocId,
    string FullName,
    string Specialty,
    DateTimeOffset CreationDateTime
);
