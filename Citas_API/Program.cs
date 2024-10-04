using Citas_API.Data;
using Citas_API.Endpoints;

var builder = WebApplication.CreateBuilder(args);
// Add CORS policy to allow all origins (for development purposes)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()    // Allow all origins (change in production)
                   .AllowAnyMethod()    // Allow any HTTP method (GET, POST, etc.)
                   .AllowAnyHeader();   // Allow any headers (Authorization, etc.)
        });
});


var dbConnString = builder.Configuration.GetConnectionString("AppCitasStore");
builder.Services.AddSqlite<AppStoreContext>(dbConnString);

var app = builder.Build();


app.UseCors("AllowAllOrigins");

app.MapAppointmentsEndpoints();
app.MapSpecialtiesEndpoints();
app.MigrateDb();

app.Run();
