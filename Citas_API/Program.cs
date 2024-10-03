using Citas_API.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapAppointmentsEndpoints();

app.Run();
