namespace Citas_API.Dtos;

public record class RequestUpdateAppointmentDTO(
    string DocIdType,
    string NumDocId,
    string FullName,
    string Specialty
);
