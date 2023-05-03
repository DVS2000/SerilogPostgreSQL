using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuração do Serilog com PostgreSQL
builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    var connectionString = hostContext.Configuration.GetConnectionString("serilogCs");
    configuration
        .Enrich.FromLogContext()
        .WriteTo.PostgreSQL(connectionString, "Logs", needAutoCreateTable: true);
});

builder.Services.AddControllers();
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

