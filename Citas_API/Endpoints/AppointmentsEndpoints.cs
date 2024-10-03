using Citas_API.Data;
using Citas_API.Dtos;
using Citas_API.Entities;
using Citas_API.Mappers;

namespace Citas_API.Endpoints;

public static class AppointmentsEndpoints
{   
    const string GetInfoCitaEndpoint = "GetInfoCita";
    static List<AppointmentDTO> listRequestsAppointment = [
        new ("DNI", "42685987", "Angel Motta", "Cardiología", DateTimeOffset.Parse("2024-10-01 15:30:00 -05:00")),
        new ("DNI", "42685987", "Angel Motta", "General", DateTimeOffset.Parse("2024-10-02 14:30:00 -05:00")),
        new ("DNI", "62685123", "Mayra Chávez", "General", DateTimeOffset.Parse("2024-10-02 16:30:00 -05:00")),
    ];

    public static RouteGroupBuilder MapAppointmentsEndpoints(this WebApplication app) {
        
        var routerGroup = app.MapGroup("/citas").WithParameterValidation();

        routerGroup.MapGet("/", () => {
            return listRequestsAppointment;
        });

        routerGroup.MapGet("/{id}", (int id) => {
            int lenList = listRequestsAppointment.Count;
            if (id > lenList - 1) {
                return Results.NotFound();
            }

            return Results.Ok(listRequestsAppointment[id]);
        }).WithName(GetInfoCitaEndpoint);

        routerGroup.MapPost("/", (RequestAppointmentDTO reqAppointment, AppStoreContext dbContext) => {
            Specialty? specialtyRequested = dbContext.Specialties.Find(reqAppointment.SpecialtyId);
            if (specialtyRequested == null)
            {
                return Results.BadRequest("Specialty not found");
            }

            Appointment newAppointment = reqAppointment.ToEntity(specialtyRequested.Id);
            
            dbContext.Appointments.Add(newAppointment);
            dbContext.SaveChanges();

            CreatedAppointmentDTO createdAppointmentResponse = newAppointment.ToDTO(specialtyRequested.Name);

            return Results.CreatedAtRoute(GetInfoCitaEndpoint, new {id = newAppointment}, createdAppointmentResponse);
        });

        routerGroup.MapPut("/{id}", (int id, RequestUpdateAppointmentDTO reqUpdateAppointment) => {
            int lenList = listRequestsAppointment.Count;
            if (id > lenList - 1) {
                return Results.NotFound();
            }

            AppointmentDTO updatedAppointment = new AppointmentDTO(
                reqUpdateAppointment.DocIdType,
                reqUpdateAppointment.NumDocId,
                reqUpdateAppointment.FullName,
                reqUpdateAppointment.Specialty,
                DateTimeOffset.Now
            );

            // Update appointment
            listRequestsAppointment[id] = updatedAppointment;

            return Results.NoContent();
        });

        routerGroup.MapDelete("/{id}", (int id) => {
            int lenList = listRequestsAppointment.Count;
            if (id > lenList - 1) {
                return Results.NotFound();
            }

            listRequestsAppointment.RemoveAt(id);

            return Results.NoContent();
        });


        return routerGroup;
    }
}
