using Citas_API.Dtos;
using Citas_API.Entities;

namespace Citas_API.Mappers;

public static class AppointmentsMapper
{
    public static Appointment ToEntity(this RequestAppointmentDTO reqAppointment, Specialty specialty) {
        Appointment newAppointment = new () {
                DocIdType = reqAppointment.DocIdType,
                NumDocId = reqAppointment.NumDocId,
                FullName = reqAppointment.FullName,
                SpecialtyId = specialty.Id,
                SpecialtyAppointment = specialty,
                CreationDateTime = DateTimeOffset.Now
        };

        return newAppointment;
    }

    public static AppointmentDTO ToDTO(this Appointment newAppointment) {
        AppointmentDTO createdAppointmentResponse = new (
                newAppointment.Id,
                newAppointment.DocIdType,
                newAppointment.NumDocId,
                newAppointment.FullName,
                newAppointment.SpecialtyId,
                newAppointment.SpecialtyAppointment.Name,
                newAppointment.CreationDateTime
        );

        return createdAppointmentResponse;
    }

    public static Appointment ToEntity(this RequestUpdateAppointmentDTO reqUpdateAppointment, int id, Specialty specialty) {
        Appointment updatedAppointment = new () {
                Id = id,
                DocIdType = reqUpdateAppointment.DocIdType,
                NumDocId = reqUpdateAppointment.NumDocId,
                FullName = reqUpdateAppointment.FullName,
                SpecialtyId = specialty.Id,
                SpecialtyAppointment = specialty,
                CreationDateTime = DateTimeOffset.Now
        };

        return updatedAppointment;
    }
}
