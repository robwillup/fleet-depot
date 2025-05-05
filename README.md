# Fleet Depot

This solution is composed of a Blazor WASM UI, a backend REST API, a CLI, class libraries and unit test projects.

## Running the FleetDepot App

.NET 9 is required for this application to run. Follow these instructions to install .NET 9 (if you don't already have it)
and to run the app.

> The `Run.ps1` and `run.sh` scripts start the API and UI in separate terminal windows.

### Windows

* Open a PowerShell terminal and navigate to the root of this repository, i.e., `fleet-depot`.
* If you don't have .NET 9 installed, run:

```PowerShell
.\scripts\dotnet-install.ps1 -Channel 9.0
```

* With .NET 9 installed, run:

```PowerShell
.\scripts\Run.ps1
```

### macOS

* Open a terminal and navigate to the root of this repository, i.e., `fleet-depot`.
* If you don't have .NET 9 installed, run:

```bash
chmod +x ./scripts/dotnet-install.sh
sudo ./scripts/dotnet-install.sh --channel 9.0
```

* With .NET 9 installed, run:

```bash
chmod +x ./scripts/run.sh
```

### Docker

If you have Docker installed, you can run the UI and API with the following command:

> NOTE: Make sure you run this command from the root of the repository, i.e., `fleet-depot/`

```bash
docker compose -f .\build\compose.yml up
```

This is going to build the images and run both apps.

## Accessing the FleetDepot App

When running locally, the UI can be accessed by navigating to the following address on your browser:

[http://localhost:5273](http://localhost:5273)

If you would like to send requests directly to the API for testing, you can access it here:

[http://localhost:5084](http://localhost:5084)

By accessing the root of the application, or [http://localhost:5084/index.html](http://localhost:5084/index.html),
you will see the list of endpoints currently available. You can also get the OpenAPI API documentation JSON at
[http://localhost:5084/swagger/v1/swagger.json](http://localhost:5084/swagger/v1/swagger.json).

## FleetDepot.Cli

As a PoC, a CLI project has been added to this solution. This project is in its very early stages and is
currently only a prototype demonstrating how to call the REST API from another application. A similar
process could be done with other application, like a mobile app for example.

To test this CLI app run:

```bash
dotnet run --project .\src\FleetDepot.Cli\FleetDepot.Cli.csproj add
```

to add a simple car object, and

```bash
dotnet run --project .\src\FleetDepot.Cli\FleetDepot.Cli.csproj
```

to list the available vehicles.
