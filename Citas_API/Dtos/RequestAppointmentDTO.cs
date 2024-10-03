using System.ComponentModel.DataAnnotations;

namespace Citas_API.Dtos;

public record class RequestAppointmentDTO(
    [Required] string DocIdType,
    [Required][StringLength(8)] string NumDocId,
    [Required] string FullName,
    [Required] string Specialty
);
