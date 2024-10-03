using Citas_API.Data;
using Citas_API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var dbConnString = builder.Configuration.GetConnectionString("AppCitasStore");
builder.Services.AddSqlite<AppStoreContext>(dbConnString);

var app = builder.Build();

app.MapAppointmentsEndpoints();

app.Run();
