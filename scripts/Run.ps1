Start-Process powershell -ArgumentList @(
    "-NoExit",
    "-Command",
    "Write-Host 'FleetDepot API is running...'; Write-Host; dotnet run --project .\src\FleetDepot.Api\FleetDepot.Api.csproj"
)

Start-Process powershell -ArgumentList @(
    "-NoExit",
    "-Command",
    "Write-Host 'FleetDepot UI is running...'; Write-Host; dotnet run --project .\src\FleetDepot.Ui\FleetDepot.Ui.csproj"
)
