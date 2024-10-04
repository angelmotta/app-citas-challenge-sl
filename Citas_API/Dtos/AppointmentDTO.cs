namespace Citas_API.Dtos;

public record class AppointmentDTO(
    int Id,
    string DocIdType,
    string NumDocId,
    string FullName,
    int SpecialtyId,
    string SpecialtyName,
    DateTimeOffset CreationDateTime
);
