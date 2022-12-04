using Microsoft.EntityFrameworkCore;
using ParkingIoT2.Data;
using ParkingIoT2.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register dbcontext for dependencies injection
builder.Services.AddDbContext<ParkingIoT2.Data.ParkingIOTDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"));
});
//Service Injection
builder.Services.AddScoped<IParkingAreaRepository, ParkingAreaRepository>();
builder.Services.AddScoped<IParkingSlotRepository, ParkingSlotRepository>();
builder.Services.AddScoped<IRFIDRepository, RFIDRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
//Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
