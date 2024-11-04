using Microsoft.EntityFrameworkCore;
using Sprint4dotnet.Data;
using Sprint4dotnet.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<YourDbContext>(options =>
{
    options.UseOracle("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521)))" +
        "(CONNECT_DATA=(SERVER=DEDICATED)(SID=ORCL)));User Id=RM99988;Password=160698;");
});

// Configura o serviço HttpClient para OpenWeather
builder.Services.AddHttpClient("openweather", client =>
{
    client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped<IClientService, ClientService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
