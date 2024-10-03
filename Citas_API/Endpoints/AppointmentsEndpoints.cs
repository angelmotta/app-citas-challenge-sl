using Citas_API.Dtos;

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
        
        var routerGroup = app.MapGroup("/citas");

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

        routerGroup.MapPost("/", (RequestAppointmentDTO reqAppointment) => {
            // TODO: validate DTO
            
            // TODO: Create full appointment (date and time)
            AppointmentDTO newAppointment = new AppointmentDTO(
                reqAppointment.DocIdType,
                reqAppointment.NumDocId,
                reqAppointment.FullName,
                reqAppointment.Specialty,
                DateTimeOffset.Now
            );
            // Store new Appointment
            listRequestsAppointment.Add(newAppointment);

            return Results.CreatedAtRoute(GetInfoCitaEndpoint, new {id = listRequestsAppointment.Count - 1}, newAppointment);
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
