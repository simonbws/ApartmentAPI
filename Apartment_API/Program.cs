
using Apartment_API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//builder.Services.AddDbContext<AppDbContext>(option =>
//{
//    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
//});
var connectionString = builder.Configuration.GetConnectionString("DefaultSQLConnection");

if(string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultSQLConnection' is not configured.");
}
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers(option =>
{
    //option.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters()
  .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
