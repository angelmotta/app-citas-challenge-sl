using Citas_API.Data;
using Citas_API.Dtos;
using Citas_API.Entities;
using Citas_API.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Citas_API.Endpoints;

public static class AppointmentsEndpoints
{   
    const string GetInfoCitaEndpoint = "GetInfoCita";
    
    public static RouteGroupBuilder MapAppointmentsEndpoints(this WebApplication app) {
        
        var routerGroup = app.MapGroup("/citas").WithParameterValidation();

        routerGroup.MapGet("/", (AppStoreContext dbContext) => {
            // return listRequestsAppointment;
            var listAppointments = dbContext.Appointments
                                        .Include(appointment => appointment.SpecialtyAppointment)
                                        .Select(appointment => appointment.ToDTO())
                                        .AsNoTracking()
                                        .ToList();

            return Results.Ok(listAppointments);
        });

        routerGroup.MapGet("/{id}", (int id, AppStoreContext dbContext) => {
            if (id < 0) return Results.NotFound();
            
            Appointment? appointment = dbContext.Appointments
                                        .Include(appointment => appointment.SpecialtyAppointment)
                                        .FirstOrDefault(appointment => appointment.Id == id);
                                                
            if (appointment == null) return Results.NotFound(); 

            AppointmentDTO responseAppointment = appointment.ToDTO();

            return Results.Ok(responseAppointment);
        }).WithName(GetInfoCitaEndpoint);

        routerGroup.MapPost("/", (RequestAppointmentDTO reqAppointment, AppStoreContext dbContext) => {
            Specialty? specialtyRequested = dbContext.Specialties.Find(reqAppointment.SpecialtyId);
            if (specialtyRequested == null) return Results.BadRequest("Specialty not found");

            Appointment newAppointment = reqAppointment.ToEntity(specialtyRequested);
            
            dbContext.Appointments.Add(newAppointment);
            dbContext.SaveChanges();

            AppointmentDTO createdAppointmentResponse = newAppointment.ToDTO();

            return Results.CreatedAtRoute(GetInfoCitaEndpoint, new {id = newAppointment}, createdAppointmentResponse);
        });

        routerGroup.MapPut("/{id}", (int id, RequestUpdateAppointmentDTO reqUpdateAppointment, AppStoreContext dbContext) => {
            // Check if appointment exists
            Appointment? existingAppointment = dbContext.Appointments
                                        .Include(appointment => appointment.SpecialtyAppointment)
                                        .FirstOrDefault(appointment => appointment.Id == id);

            if (existingAppointment == null) return Results.NotFound(); // HTTP 404

            // Validate if `specialty` requested is valid
            Specialty? specialtyRequested = dbContext.Specialties.Find(reqUpdateAppointment.SpecialtyId);
            if (specialtyRequested == null) return Results.BadRequest("Specialty not found");
            
            // Prepare entity
            Appointment updatedAppointment = reqUpdateAppointment.ToEntity(id, specialtyRequested);

            // Update entity in DB
            dbContext.Entry(existingAppointment).CurrentValues.SetValues(updatedAppointment);
            dbContext.SaveChanges();
            
            return Results.NoContent(); // HTTP 204 
        });

        routerGroup.MapDelete("/{id}", (int id, AppStoreContext dbContext) => {
            var numDeletedRows = dbContext.Appointments
                                        .Where(appointment => appointment.Id == id)
                                        .ExecuteDelete();

            if (numDeletedRows == 0) return Results.NotFound();

            return Results.NoContent(); // HTTP 204 
        });


        return routerGroup;
    }
}
