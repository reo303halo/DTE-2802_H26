# POI Map

ASP.NET Core MVC (.NET 10) app that shows points of interest on a map, with create, edit and delete.

## Run

    dotnet run

Then open http://localhost:5290. The SQLite database (`poimap.db`) and seed data are created on first
run. Delete `poimap.db` to start over. An internet connection is needed, because the map tiles and the
frontend libraries (Leaflet, Bootstrap, jQuery) are loaded from external servers.

## Structure

| Folder         | Purpose                                                        |
| -------------- | -------------------------------------------------------------- |
| `Models`       | The `PointOfInterest` entity and the `PoiCategory` enum        |
| `Data`         | `AppDbContext`: EF Core configuration and seed data            |
| `Repositories` | `IPoiRepository` and `PoiRepository`: all database access      |
| `ViewModels`   | Data for the views, and mapping to and from the entity         |
| `Controllers`  | `PoiController`: pages, form handling and the JSON endpoint    |
| `Views`        | Razor views                                                    |
| `wwwroot/js`   | `poi-map.js` (map page), `poi-picker.js` (position on the form) |

Request flow for the map: the browser loads `Poi/Index`, `poi-map.js` calls `GET /Poi/Markers`, the
controller gets the entities from the repository and maps them to `PoiViewModel`, and the JSON result
becomes Leaflet markers.

## Using migrations

The app uses `EnsureCreated()` so that it runs without extra steps. It cannot update an existing
database, so switch to migrations before changing the model:

1. Delete `poimap.db`.
2. In `Program.cs`, replace `context.Database.EnsureCreated();` with `context.Database.Migrate();`.
3. Run:

       dotnet tool install --global dotnet-ef
       dotnet ef migrations add InitialCreate

The migration is then applied at startup.
