using System;
using Citas_API.Dtos;
using Citas_API.Entities;

namespace Citas_API.Mappers;

public static class SpecialtiesMapper
{
    public static SpecialtyDTO ToDTO(this Specialty specialty) {
        return new SpecialtyDTO(
            specialty.Id,
            specialty.Name
        );
    }
}
