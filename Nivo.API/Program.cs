using Microsoft.EntityFrameworkCore;
using Nivo.API.Data;
using Nivo.API.Repositories.Implementation;
using Nivo.API.Repositories.Interface;
using Nivo.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NivoConnectionString")));
    
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<AppointmentService>();

Nivo.API.Extensions.OpenApiExtensions.AddOpenApi(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Nivo.API.Extensions.OpenApiExtensions.MapOpenApi(app);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();