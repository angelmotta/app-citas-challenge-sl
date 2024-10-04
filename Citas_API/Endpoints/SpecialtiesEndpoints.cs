using System;
using Citas_API.Data;
using Citas_API.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Citas_API.Endpoints;

public static class SpecialtiesEndpoints
{
    public static RouteGroupBuilder MapSpecialtiesEndpoints(this WebApplication app) {
        var routerGroup = app.MapGroup("/especialidades").WithParameterValidation();

        routerGroup.MapGet("/", async (AppStoreContext dbContext) => {
            // return listRequestsAppointment;
            var listSpecialties = await dbContext.Specialties
                                        .Select(rowObj => rowObj.ToDTO())
                                        .AsNoTracking()
                                        .ToListAsync();

            return Results.Ok(listSpecialties);
        });

        return routerGroup;
    }
}
