# Background Processing Service for ASP.NET Core

A simple ASP.NET Core Web API project that demonstrates how to run a hosted background service in .NET 6.

## What it does

- Hosts an ASP.NET Core Web API with Swagger/OpenAPI enabled for development.
- Registers a hosted service: `DoSomethingEveryXHoursBackgroundService`.
- The background service writes a log message to the console on each execution cycle.
- The execution delay is configurable through `appsettings.json`.

## Key files

- `Program.cs` - ASP.NET Core application setup and service registration.
- `Tasks/DoSomethingEveryXHoursBackgroundService.cs` - Hosted background service implementation.
- `Services/ConfigurationService.cs` - Reads background service configuration from app settings.
- `appsettings.json` - Default configuration values, including `BackgroundService1:WorkerDelaySeconds`.

## Configuration

The background service delay is configured in `appsettings.json` using:

```json
"BackgroundService1": {
  "WorkerDelaySeconds": 10
}
```

If the configuration value is missing or invalid, the service defaults to `10` seconds.

## Run the app

From the project root:

```bash
dotnet run
```

Open the Swagger UI when running in Development mode:

```text
https://localhost:5001/swagger
```

## Background service behavior

The hosted service repeatedly executes `DoWorkAsync()` until the application shuts down.
Each iteration is followed by a delay of `WorkerDelaySeconds` seconds. The current implementation writes a timestamped message to the console.

## Notes

- `IConfigurationService` is registered as a scoped service and used by the hosted service.
- `NLog` is included as a dependency and the hosted service logs exceptions using `LogManager.GetCurrentClassLogger()`.
- Swagger is enabled only in development to simplify testing of the API.

## References

- [Using Hosted Services in ASP.NET Core to create a "most viewed" background service](https://www.roundthecode.com/dotnet-tutorials/hosted-services-asp-net-core-create-a-most-viewed-background-service)
- [Use a .NET Worker Service to run background services](https://www.roundthecode.com/dotnet-tutorials/use-dotnet-worker-service-run-background-services)
