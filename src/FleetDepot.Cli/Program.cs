using System.Net.Http.Json;
using System.Text.Json;
using FleetDepot.Types.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();
builder.Services.AddHttpClient();
using IHost host = builder.Build();

if (args.Length == 0)
{
    await WithHttpClientAsync(host.Services, ListAsync);
}
else if (args[0] == "add")
{
    await WithHttpClientAsync(host.Services, AddOneAsync);
}

static async Task WithHttpClientAsync(IServiceProvider services, Func<HttpClient, Task> action)
{
    using var scope = services.CreateScope();
    var client = scope.ServiceProvider.GetRequiredService<HttpClient>();
    await action(client);
}

static async Task ListAsync(HttpClient httpClient)
{
    var response = await httpClient.GetAsync("http://localhost:5084/api/vehicles");
    string content = await response.Content.ReadAsStringAsync();

    var vehicles = JsonSerializer.Deserialize<List<VehicleDto>>(content) ?? [];
    foreach (var vehicle in vehicles)
    {
        Console.WriteLine($"Type: {vehicle.Type}, Number of Passengers: {vehicle.NumberOfPassengers}, Color: {vehicle.Color}");
    }
}

static async Task AddOneAsync(HttpClient httpClient)
{
    CarDto car = new() { Series = Guid.NewGuid().ToString(), Number = 1, Color = "green" };
    _ = await httpClient.PostAsJsonAsync("http://localhost:5084/api/vehicles/cars", car);
}
