using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Host.ConfigureAppConfiguration((host, config) =>
{
  config.SetBasePath(host.HostingEnvironment.ContentRootPath).AddJsonFile("Configurations/ocelot.json", optional: false, reloadOnChange: true).AddEnvironmentVariables();
});

builder.Services.AddOcelot(); // ocelot hizmetini service olarak sisteme ekledik.
// ocelot ile ilgiki config dosyas� i�ine yaz�lm�� servisleri sisteme tan�tmam�z� sa�layan registeration servis


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.UseOcelot(); // ocelot config �zerinden requestlerin y�nlendirlmesi i�in yaz�lm�� bir ara yaz�l�m.

app.Run();
