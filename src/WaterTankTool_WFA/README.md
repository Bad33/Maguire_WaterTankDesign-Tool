# WaterTankTool_WFA

Windows Forms desktop application for configuring and analyzing elevated water tank designs (single-column and multi-column variants), including load inputs, segment/material definitions, solver outputs, and drawing/export workflows.

## Tech Stack

- .NET 8 (`net8.0-windows`)
- Windows Forms
- Entity Framework Core + SQLite
- Newtonsoft.Json / System.Text.Json
- iTextSharp (PDF-related utilities)

## Solution Structure

- `WaterTankTool_WFA.sln` - Visual Studio solution
- `WaterTankTool_WFA/WaterTankTool_WFA.csproj` - Main project
- `WaterTankTool_WFA/StartupForm.cs` - Project chooser / creator and recent-project launcher
- `WaterTankTool_WFA/WaterTank.cs` - Main design workspace UI
- `WaterTankTool_WFA/Solver Equation/` - Core engineering equations
- `WaterTankTool_WFA/Load/` - Load input screens (wind, seismic, snow, live, dead, combinations)
- `WaterTankTool_WFA/Segments/` - Segment definition UIs
- `WaterTankTool_WFA/Materials/` - Material definition and storage
- `WaterTankTool_WFA/Solver/` - Allowable stress, design table, and solver output views
- `WaterTankTool_WFA/Entity/` - EF entities for persisted design data
- `WaterTankTool_WFA/Migrations/` - EF migration history

## Prerequisites

- Windows 10/11
- .NET 8 SDK
- Visual Studio 2022 recommended

## Build and Run

From repository root:

```powershell
dotnet restore .\src\WaterTankTool_WFA\WaterTankTool_WFA.sln
dotnet build .\src\WaterTankTool_WFA\WaterTankTool_WFA.sln -c Debug
dotnet run --project .\src\WaterTankTool_WFA\WaterTankTool_WFA\WaterTankTool_WFA.csproj
```

## Runtime Data and Persistence

- Tank templates:
  - `WaterTankTool_WFA/tanks.json`
  - `WaterTankTool_WFA/MultiLeg-Tanks.json`
- Per-project workspace files (created when user creates a project):
  - `<ProjectFolder>/<ProjectName>.proj`
  - `<ProjectFolder>/project.json`
  - `<ProjectFolder>/project_data.db` (SQLite)
- Recent projects:
  - `recent_projects.json` (app working directory)
- App-level state:
  - `%AppData%/WaterTankTool_WFA/appstate.json`
- Designer notes:
  - `notes.json` (application base directory)

## Typical User Flow

1. Select tank type (single-column or multi-column).
2. Create/open a project workspace.
3. Define geometry segments and material properties.
4. Enter load cases (wind/seismic/snow/live/dead).
5. Review solver/design tables and outputs.
6. Export drawings/results.

## Developer Notes

- The app uses a shared `WaterTankDbContext` instance initialized per opened project.
- Database bootstrap is handled in `StartupForm.InitializeProjectDatabase(...)` and `EnsureCreated()`.
- Migrations are included, but startup logic also supports direct schema creation for new project databases.

## Troubleshooting

- If startup fails after selecting a project, confirm the selected folder is writable and contains a valid `project.json`.
- If load/material views appear empty, check whether `project_data.db` was created in the project folder.
- If UI scaling looks incorrect on high-DPI screens, verify Windows display scaling and run with latest .NET 8 runtime.
