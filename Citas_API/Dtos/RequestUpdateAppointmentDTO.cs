using System.ComponentModel.DataAnnotations;

namespace Citas_API.Dtos;

public record class RequestUpdateAppointmentDTO(
    [Required] string DocIdType,
    [Required][StringLength(8)] string NumDocId,
    [Required] string FullName,
    [Required] int? SpecialtyId
);
