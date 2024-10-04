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

        routerGroup.MapGet("/", async (AppStoreContext dbContext) => {
            // return listRequestsAppointment;
            var listAppointments = await dbContext.Appointments
                                        .Include(appointment => appointment.SpecialtyAppointment)
                                        .Select(appointment => appointment.ToDTO())
                                        .AsNoTracking()
                                        .ToListAsync();

            return Results.Ok(listAppointments);
        });

        routerGroup.MapGet("/{id}", async (int id, AppStoreContext dbContext) => {
            if (id < 0) return Results.NotFound();
            
            Appointment? appointment = await dbContext.Appointments
                                        .Include(appointment => appointment.SpecialtyAppointment)
                                        .FirstOrDefaultAsync(appointment => appointment.Id == id);
                                                
            if (appointment == null) return Results.NotFound(); 

            AppointmentDTO responseAppointment = appointment.ToDTO();

            return Results.Ok(responseAppointment);
        }).WithName(GetInfoCitaEndpoint);

        routerGroup.MapPost("/", async (RequestAppointmentDTO reqAppointment, AppStoreContext dbContext) => {
            Specialty? specialtyRequested = await dbContext.Specialties.FindAsync(reqAppointment.SpecialtyId);
            if (specialtyRequested == null) return Results.BadRequest("Specialty not found");

            Appointment newAppointment = reqAppointment.ToEntity(specialtyRequested);
            
            dbContext.Appointments.Add(newAppointment);
            await dbContext.SaveChangesAsync();

            AppointmentDTO createdAppointmentResponse = newAppointment.ToDTO();

            return Results.CreatedAtRoute(GetInfoCitaEndpoint, new {id = newAppointment}, createdAppointmentResponse);
        });

        routerGroup.MapPut("/{id}", async (int id, RequestUpdateAppointmentDTO reqUpdateAppointment, AppStoreContext dbContext) => {
            // Check if appointment exists
            Appointment? existingAppointment = await dbContext.Appointments
                                        .Include(appointment => appointment.SpecialtyAppointment)
                                        .FirstOrDefaultAsync(appointment => appointment.Id == id);

            if (existingAppointment == null) return Results.NotFound(); // HTTP 404

            // Validate if `specialty` requested is valid
            Specialty? specialtyRequested = await dbContext.Specialties.FindAsync(reqUpdateAppointment.SpecialtyId);
            if (specialtyRequested == null) return Results.BadRequest("Specialty not found");
            
            // Prepare entity
            Appointment updatedAppointment = reqUpdateAppointment.ToEntity(id, specialtyRequested);

            // Update entity in DB
            dbContext.Entry(existingAppointment).CurrentValues.SetValues(updatedAppointment);
            await dbContext.SaveChangesAsync();
            
            return Results.NoContent(); // HTTP 204 
        });

        routerGroup.MapDelete("/{id}", async (int id, AppStoreContext dbContext) => {
            var numDeletedRows = await dbContext.Appointments
                                        .Where(appointment => appointment.Id == id)
                                        .ExecuteDeleteAsync();

            if (numDeletedRows == 0) return Results.NotFound();

            return Results.NoContent(); // HTTP 204 
        });


        return routerGroup;
    }
}
