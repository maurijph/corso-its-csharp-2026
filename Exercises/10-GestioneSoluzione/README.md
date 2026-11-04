# Esercitazione 10 — Gestione della soluzione in .NET

Una **soluzione** (`.sln`) è un contenitore che raggruppa più **progetti** (`.csproj`).
Qui dentro ce ne sono due, entrambi un semplice "Hello, World!":

| Progetto | Tipo | Cosa fa |
|---|---|---|
| `ProgettoConsole` | console | Scrive una riga nel terminale e termina |
| `ProgettoWebAPI` | Web API | Resta acceso e risponde `Hello, World!` all'indirizzo `/` |

```text
10-GestioneSoluzione/
├── NomeSoluzione.sln
├── ProgettoConsole/
│   ├── ProgettoConsole.csproj
│   └── Program.cs
└── ProgettoWebAPI/
    ├── ProgettoWebAPI.csproj
    └── Program.cs
```

## Come è stata creata (i comandi della slide)

Rifalla da zero nella tua cartella `MyExercises/`, un comando alla volta:

```powershell
# 1. Creare una nuova soluzione vuota
dotnet new sln -n NomeSoluzione --format sln

# 2. Creare i progetti
dotnet new console -n ProgettoConsole
dotnet new webapi -n ProgettoWebAPI

# 3. Aggiungere i progetti alla soluzione
dotnet sln NomeSoluzione.sln add ./ProgettoConsole/ProgettoConsole.csproj
dotnet sln NomeSoluzione.sln add ./ProgettoWebAPI/ProgettoWebAPI.csproj

# 4. Verificare i progetti inclusi nella soluzione
dotnet sln NomeSoluzione.sln list
```

> **Perché `--format sln`?** Dalla versione 10 di .NET, `dotnet new sln` crea un file
> `.slnx` (il nuovo formato). Con `--format sln` otteniamo il classico `NomeSoluzione.sln`,
> lo stesso nome usato nei comandi della slide.

## Come provarla

Dalla cartella della soluzione:

```powershell
# Compila TUTTI i progetti della soluzione in un colpo solo
dotnet build NomeSoluzione.sln

# Avvia il progetto console
dotnet run --project ProgettoConsole

# Avvia la Web API (si ferma con Ctrl+C)
dotnet run --project ProgettoWebAPI
```

Con la Web API accesa, apri nel browser l'indirizzo che compare nel terminale
(es. `http://localhost:5106`) e leggerai `Hello, World! (da ProgettoWebAPI)`.

In Visual Studio basta fare doppio clic su `NomeSoluzione.sln`: si aprono entrambi i progetti insieme.

---

## English version

# Exercise 10 — Managing the solution in .NET

A **solution** (`.sln`) is a container that groups several **projects** (`.csproj`).
This one holds two of them, both a simple "Hello, World!":

| Project | Type | What it does |
|---|---|---|
| `ProgettoConsole` | console | Writes a line in the terminal and ends |
| `ProgettoWebAPI` | Web API | Stays on and answers `Hello, World!` at the `/` address |

```text
10-GestioneSoluzione/
├── NomeSoluzione.sln
├── ProgettoConsole/
│   ├── ProgettoConsole.csproj
│   └── Program.cs
└── ProgettoWebAPI/
    ├── ProgettoWebAPI.csproj
    └── Program.cs
```

### How it was created (the slide commands)

Rebuild it from scratch in your `MyExercises/` folder, one command at a time:

```powershell
# 1. Create a new empty solution
dotnet new sln -n NomeSoluzione --format sln

# 2. Create the projects
dotnet new console -n ProgettoConsole
dotnet new webapi -n ProgettoWebAPI

# 3. Add the projects to the solution
dotnet sln NomeSoluzione.sln add ./ProgettoConsole/ProgettoConsole.csproj
dotnet sln NomeSoluzione.sln add ./ProgettoWebAPI/ProgettoWebAPI.csproj

# 4. Check the projects included in the solution
dotnet sln NomeSoluzione.sln list
```

> **Why `--format sln`?** Since .NET 10, `dotnet new sln` creates a `.slnx` file
> (the new format). With `--format sln` we get the classic `NomeSoluzione.sln`,
> the same name used in the slide commands.

### How to try it

From the solution folder:

```powershell
# Build ALL the projects of the solution in one go
dotnet build NomeSoluzione.sln

# Start the console project
dotnet run --project ProgettoConsole

# Start the Web API (stop it with Ctrl+C)
dotnet run --project ProgettoWebAPI
```

With the Web API running, open in the browser the address shown in the terminal
(e.g. `http://localhost:5106`) and you will read `Hello, World! (da ProgettoWebAPI)`.

In Visual Studio just double-click `NomeSoluzione.sln`: both projects open together.
