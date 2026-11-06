# ✦ NordlysLogg

Et eksempelprosjekt i **ASP.NET Core MVC (.NET 10)** for logging av nordlysobservasjoner.
Prosjektet viser Models, ViewModels, Views, Entity Framework Core (SQLite) og repository-mønsteret.

## Kom i gang

```bash
dotnet restore
dotnet run
```

Databasen `nordlys.db` lages automatisk ved første oppstart, med startdata
(4 steder, 3 observatører og 8 observasjoner).

### Med migrasjoner (anbefalt i undervisning)

```bash
dotnet tool install --global dotnet-ef      # én gang
rm nordlys.db                                # hvis den allerede er laget med EnsureCreated
dotnet ef migrations add InitialCreate
dotnet run                                   # Program.cs kjører nå Migrate() automatisk
```

`Program.cs` sjekker om det finnes migrasjoner: finnes de, brukes `Migrate()`, ellers `EnsureCreated()`.

## Struktur

| Mappe | Innhold |
|---|---|
| `Models/` | Domene-entiteter: `AuroraSighting`, `ObservationSite`, `Observer`, enumer |
| `Data/` | `NordlysContext` (DbContext, Fluent API) og `SeedData` (`HasData`) |
| `Repositories/` | Grensesnitt + EF-implementasjoner, registrert med DI i `Program.cs` |
| `ViewModels/` | Skjema-, liste-, detalj- og dashboard-modeller tilpasset hvert view |
| `Controllers/` | `Home` (dashboard), `Sightings` (full CRUD), `Sites` (liste, detaljer, opprett) |
| `Views/` | Razor-views, partials (`_SightingCard`, `_WowBadge`, `_SightingForm`) og layout |

## Undervisningspoeng i koden

- **ViewModel vs. entitet** – `SightingsController` bruker `SightingFormViewModel` (med nedtrekkslister,
  egen validering og mapping), mens `SitesController.Create` binder entiteten direkte med `[Bind]`.
  Fin kontrast til diskusjon om overposting.
- **Repository + DI** – controllerne avhenger kun av `ISightingRepository` osv., aldri av `DbContext`.
- **`IValidatableObject`** – tidspunkt i fremtiden avvises; Korona ved Kp < 3 gir advarsel.
- **`[NotMapped]`** – `WowScore` beregnes i modellen og lagres ikke. `GetMostSpectacularAsync()` må derfor
  sortere i minnet – godt utgangspunkt for å snakke om hva som kan oversettes til SQL.
- **EF-detaljer** – `Include`/`ThenInclude`, `AsNoTracking`, enum lagret som tekst (`HasConversion<string>()`),
  `DeleteBehavior.Restrict` vs. `Cascade`, indeks på `ObservedAt`.
- **Post/Redirect/Get** med `TempData`-meldinger.
- **GET-skjema** for filtrering, så filteret havner i URL-en.

## Forslag til studentoppgaver

1. Lag `Edit` og `Delete` for steder (hva skjer når et sted med observasjoner slettes?).
2. Lag CRUD for `Observer` med egen ViewModel.
3. Legg til mange-til-mange: et `Tag`-objekt (f.eks. «speiling», «fullmåne», «bilde tatt»).
4. Skriv enhetstester for `SightingsController` med en fake `ISightingRepository`.
5. Flytt Wow-score-beregningen til en tjeneste (`IWowScoreCalculator`) og injiser den.
6. Legg til paginering i observasjonslisten.

> Bootstrap og jQuery-validering lastes fra CDN, så det trengs ikke `libman`/`wwwroot/lib`.
