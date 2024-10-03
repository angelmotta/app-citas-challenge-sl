using Citas_API.Dtos;
using Citas_API.Entities;

namespace Citas_API.Mappers;

public static class AppointmentsMapper
{
    public static Appointment ToEntity(this RequestAppointmentDTO reqAppointment, int speciltyId) {
        Appointment newAppointment = new () {
                DocIdType = reqAppointment.DocIdType,
                NumDocId = reqAppointment.NumDocId,
                FullName = reqAppointment.FullName,
                SpecialtyId = speciltyId,
                CreationDateTime = DateTimeOffset.Now
        };

        return newAppointment;
    }

    public static CreatedAppointmentDTO ToDTO(this Appointment newAppointment, string specialtyName) {
        CreatedAppointmentDTO createdAppointmentResponse = new (
                newAppointment.DocIdType,
                newAppointment.NumDocId,
                newAppointment.FullName,
                specialtyName,
                newAppointment.CreationDateTime
        );

        return createdAppointmentResponse;
    }
}
