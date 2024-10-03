namespace Citas_API.Dtos;

public record class RequestAppointmentDTO(
    string DocIdType,
    string NumDocId,
    string FullName,
    string Specialty
);
