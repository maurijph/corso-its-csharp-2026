# Esercitazione 9.1 — API CRUD di piloti con il modello CSR

> **Questa esercitazione la fai da solo.** In questa cartella c'è solo la traccia:
> il progetto lo crei tu e il codice lo scrivi tu. Come modello usa l'esercitazione 9
> (CrudPersoneCSR): è lo stesso lavoro, fatto sulle persone invece che sui piloti.

## Che cosa facciamo

Nell'esercitazione 8.1 hai scritto un'API CRUD di **piloti di F1** con tutto il codice
dentro `Program.cs`: gli endpoint, le regole e il dizionario con i dati.

Qui **non aggiungi quasi niente di nuovo**: prendi la stessa API e la **riorganizzi**.
Questo lavoro si chiama **refactoring**: il codice cambia forma, ma chi usa l'API non
si accorge di nulla. Gli indirizzi, i verbi HTTP e i codici di risposta restano
quelli dell'esercitazione 8.1.

È esattamente quello che abbiamo fatto passando dall'esercitazione 8 (CrudPersone)
all'esercitazione 9 (CrudPersoneCSR).

## Il modello CSR in breve

**CSR** sta per **C**ontroller – **S**ervice – **R**epository: il codice viene diviso
in tre livelli, e ognuno ha **un solo compito**.

| Livello | Cartella | Classe | Che cosa fa | Che cosa **non** fa |
|---|---|---|---|---|
| **Controller** | `Endpoints` | `PilotaEndpoints` | riceve la richiesta HTTP e sceglie il codice di risposta (200, 400, 404) | non contiene regole, non tocca i dati |
| **Service** | `Services` | `PilotaService` | contiene le **regole** del programma (es. "il nome è obbligatorio") | non sa nulla di HTTP né di dove sono salvati i dati |
| **Repository** | `Repositories` | `PilotaRepository` | legge e scrive i dati nel `Dictionary` | non contiene regole |

Ogni livello parla **solo con quello subito sotto**:

```
client  ->  Controller  ->  Service  ->  Repository  ->  dati
```

Il Controller non chiama mai il Repository direttamente, e il Repository non sa
nemmeno che esiste HTTP.

## L'API da ottenere

Gli endpoint sono quelli dell'esercitazione 8.1, **identici**:

| Operazione | Verbo HTTP | Indirizzo | Risposte |
|---|---|---|---|
| Leggi tutti i piloti | `GET` | `/piloti` | `200` con l'elenco (anche vuoto: `[]`) |
| Leggi un pilota | `GET` | `/piloti/{id}` | `200` con il pilota, `404` se non esiste |
| Leggi i piloti di una scuderia | `GET` | `/piloti/scuderia/{nome}` | `200` con l'elenco (anche vuoto) |
| Aggiungi un pilota | `POST` | `/piloti` | `200`, `400` se i dati non vanno bene |
| Modifica un pilota | `PUT` | `/piloti` | `200`, `404` se l'`id` non esiste o i dati non vanno bene |
| Cancella un pilota | `DELETE` | `/piloti/{id}` | `200`, `404` se l'`id` non esiste |

> Il filtro per scuderia era un **bonus** dell'esercitazione 8.1: qui è **obbligatorio**,
> perché è un ottimo esempio di regola che deve stare nel Service.

## Passo 1 — Crea la cartella e il progetto

Ricorda: il tuo lavoro va dentro `MyExercises`, non dentro `Exercises`.
Apri un terminale nella cartella principale del repository e scrivi:

```bash
cd MyExercises
mkdir 9.1-CrudPilotiCSR
cd 9.1-CrudPilotiCSR
dotnet new webapi -f net8.0
```

Come sempre: niente `-n`, e `-f net8.0` per usare .NET 8.

Poi apri `9.1-CrudPilotiCSR.csproj` e controlla le versioni dei pacchetti, come
nell'esercitazione 8.1:

| Pacchetto | Versione giusta per .NET 8 |
|---|---|
| `Microsoft.AspNetCore.OpenApi` | deve iniziare con **8** (es. `8.0.11`) |
| `Swashbuckle.AspNetCore` | deve iniziare con **6** (es. `6.9.0`) |

## Passo 2 — Copia la classe `Pilota`

La classe `Pilota` non cambia: copia il tuo **`Pilota.cs`** dall'esercitazione 8.1
e mettilo accanto a `Program.cs`.

## Passo 3 — Crea le tre cartelle

Dentro il progetto crea le cartelle:

```
9.1-CrudPilotiCSR/
├── Endpoints/
├── Services/
├── Repositories/
├── Pilota.cs
└── Program.cs
```

Conviene scrivere i livelli **dal basso verso l'alto**: prima il Repository (che non
dipende da nessuno), poi il Service, infine il Controller.

## Passo 4 — Il Repository: `Repositories/PilotaRepository.cs`

Crea la classe `public class PilotaRepository`. È l'**unica** classe che vede il
dizionario dei piloti.

1. Sposta qui il **`Dictionary<int, Pilota>`** dal vecchio `Program.cs` e rendilo
   **`private`**: nessun altro livello deve poterlo toccare.
2. Riempilo subito con i due piloti di partenza (Leclerc e Norris), come nella 8.1.
3. Scrivi questi metodi:

| Metodo | Restituisce |
|---|---|
| `GetPiloti()` | `IEnumerable<Pilota>`: tutti i piloti |
| `GetPilota(int id)` | `Pilota?`: il pilota, oppure `null` se non esiste |
| `Create(Pilota pilota)` | `bool`: `false` se l'`id` c'è già, altrimenti lo aggiunge e restituisce `true` |
| `Update(Pilota pilota)` | `bool`: `false` se l'`id` non c'è, altrimenti lo sostituisce e restituisce `true` |
| `Delete(int id)` | `bool`: `false` se l'`id` non c'è, altrimenti lo cancella e restituisce `true` |

Modello: `PersonRepository.cs` dell'esercitazione 9.

## Passo 5 — Il Service: `Services/PilotaService.cs`

Crea la classe `public class PilotaService`. Al suo interno crea il Repository:

```csharp
private PilotaRepository pilotaRepository = new PilotaRepository();
```

Poi scrivi gli **stessi metodi** del Repository (`GetPiloti`, `GetPilota`, `Create`,
`Update`, `Delete`), che chiamano il Repository, più quello per la scuderia:

- **`GetPilotiDiScuderia(string nome)`** — prende tutti i piloti dal Repository e tiene
  solo quelli della scuderia indicata. Il confronto non deve badare a maiuscole e
  minuscole: `"ferrari"` e `"Ferrari"` sono la stessa scuderia.

Le **regole** vanno tutte qui. Scrivi un metodo **`private bool IsValid(Pilota pilota)`**
e usalo sia in `Create` sia in `Update`: se i dati non sono validi, restituisci `false`
**senza nemmeno chiamare** il Repository.

Un pilota è valido se:

1. il **`Nome`** non è vuoto (suggerimento: `string.IsNullOrWhiteSpace`);
2. la **`Scuderia`** non è vuota;
3. il **`Numero`** di gara è compreso tra **1** e **99**.

C'è poi una regola che vale **solo per `Create`**:

4. il **`Numero`** di gara non deve essere già usato da un altro pilota (era il secondo
   bonus della 8.1).

Modello: `PersonService.cs` dell'esercitazione 9 (guarda `IsValid` e `GetAdults`).

## Passo 6 — Il Controller: `Endpoints/PilotaEndpoints.cs`

Crea la classe **`public static class PilotaEndpoints`**. Al suo interno:

1. Un campo **`private static PilotaService pilotaService = new PilotaService();`**.
   È `static` perché deve esserci **un solo** Service (e quindi un solo Repository) per
   tutta la vita dell'API: altrimenti i dati andrebbero persi.
2. Un metodo di estensione **`public static void MapPilotiEndpoints(this IEndpointRouteBuilder app)`**
   che contiene i sei `app.MapGet`, `app.MapPost`, `app.MapPut`, `app.MapDelete`.

Ogni endpoint fa **solo due cose**: chiama il Service e trasforma il risultato in una
risposta HTTP. Per esempio, se `pilotaService.Create(pilota)` restituisce `true`
rispondi `Results.Ok()`, altrimenti `Results.BadRequest()`.

Modello: `PersonEndpoints.cs` dell'esercitazione 9.

## Passo 7 — Ripulisci `Program.cs`

Adesso `Program.cs` deve restare **cortissimo**: niente dizionario, niente endpoint.
Solo il builder con Swagger, `builder.Build()`, la chiamata

```csharp
app.MapPilotiEndpoints();
```

l'attivazione di Swagger e `app.Run()`. Confrontalo con il `Program.cs`
dell'esercitazione 9: devono essere quasi uguali.

## Passo 8 — Il file `.http`

Avvia l'API con `dotnet run` e, da un **secondo terminale** nella cartella del progetto,
genera il file delle richieste come nell'esercitazione 8.1:

```bash
httpgenerator http://localhost:<porta>/swagger/v1/swagger.json --output-type OneFile --output .
```

Poi compila le richieste con dati veri.

## Che cosa provare

Guarda ogni volta il **codice di risposta**:

1. `GET /piloti`: ci sono i due piloti di partenza.
2. `GET /piloti/1`: `200` con Leclerc. `GET /piloti/99`: `404`.
3. `POST /piloti` con questo corpo:
   ```json
   { "id": 3, "nome": "Lewis Hamilton", "scuderia": "Ferrari", "numero": 44 }
   ```
   Poi ripeti la stessa richiesta: adesso deve rispondere `400`.
4. `GET /piloti/scuderia/ferrari`: Leclerc e Hamilton.
5. `POST /piloti` con `"nome": ""`: `400`. Con `"numero": 150`: `400`.
   Con `"numero": 16` (già di Leclerc): `400`.
6. `PUT /piloti` con `id` 3 e un numero diverso, poi rileggilo con `GET /piloti/3`.
7. `PUT /piloti` con `id` 99: `404`.
8. `DELETE /piloti/3`, poi prova a cancellarlo una seconda volta: `404`.

Se hai fatto bene il refactoring, i punti 1, 2, 3, 6, 7 e 8 rispondono **esattamente**
come nell'esercitazione 8.1.

## Che cosa consegnare

La cartella `MyExercises/9.1-CrudPilotiCSR` con:

- `Pilota.cs`
- `Repositories/PilotaRepository.cs`
- `Services/PilotaService.cs`
- `Endpoints/PilotaEndpoints.cs`
- `Program.cs` ripulito
- il file `.http` con le richieste compilate con dati veri

## Domande a cui rispondere

- Il controllo "l'`id` esiste già?" sta nel **Repository**, mentre "il numero di gara
  è già usato?" sta nel **Service**. Perché? Qual è la differenza tra un **vincolo dei
  dati** e una **regola del programma**?
- Se domani i piloti venissero salvati in un database invece che in un `Dictionary`,
  quali file dovresti cambiare? E quali resterebbero identici?
- Che cosa succederebbe se nel Controller togliessi `static` dal campo `pilotaService`?
- Con `PUT` e dati non validi l'API risponde `404`, come se il pilota non esistesse.
  Ti sembra corretto? Come potresti fare per distinguere i due casi?

## Bonus (per chi finisce prima)

- **Classifica per numero.** Aggiungi `GET /piloti/ordinati`, che restituisce i piloti
  ordinati per `Numero` di gara. L'ordinamento è una regola: in quale livello va scritto?
- **Numero unico anche in `PUT`.** Nella modifica, rispondi con un errore se il nuovo
  `Numero` è già usato da un **altro** pilota (attenzione: non dal pilota stesso!).

---

## English version

# Exercise 9.1 — Drivers CRUD API with the CSR pattern

> **You do this exercise on your own.** This folder only holds the outline:
> you create the project and you write the code. Use exercise 9 (CrudPersoneCSR)
> as a model: it is the same work, done on people instead of drivers.

### What we do

In exercise 8.1 you wrote a CRUD API of **F1 drivers** with all the code inside
`Program.cs`: the endpoints, the rules and the dictionary with the data.

Here **you add almost nothing new**: you take the same API and **reorganise** it.
This work is called **refactoring**: the code changes shape, but whoever uses the API
does not notice anything. Addresses, HTTP verbs and response codes stay the same as
in exercise 8.1.

It is exactly what we did going from exercise 8 (CrudPersone) to exercise 9
(CrudPersoneCSR).

### The CSR pattern in short

**CSR** stands for **C**ontroller – **S**ervice – **R**epository: the code is split
into three layers, and each one has **a single job**.

| Layer | Folder | Class | What it does | What it does **not** do |
|---|---|---|---|---|
| **Controller** | `Endpoints` | `PilotaEndpoints` | receives the HTTP request and picks the response code (200, 400, 404) | holds no rules, never touches the data |
| **Service** | `Services` | `PilotaService` | holds the program **rules** (e.g. "the name is required") | knows nothing about HTTP or where the data is stored |
| **Repository** | `Repositories` | `PilotaRepository` | reads and writes the data in the `Dictionary` | holds no rules |

Each layer only talks to **the one right below it**:

```
client  ->  Controller  ->  Service  ->  Repository  ->  data
```

The Controller never calls the Repository directly, and the Repository does not even
know HTTP exists.

### The API to build

The endpoints are the ones of exercise 8.1, **identical**:

| Operation | HTTP verb | Address | Responses |
|---|---|---|---|
| Read all drivers | `GET` | `/piloti` | `200` with the list (even empty: `[]`) |
| Read one driver | `GET` | `/piloti/{id}` | `200` with the driver, `404` if it does not exist |
| Read the drivers of a team | `GET` | `/piloti/scuderia/{nome}` | `200` with the list (even empty) |
| Add a driver | `POST` | `/piloti` | `200`, `400` if the data is not acceptable |
| Change a driver | `PUT` | `/piloti` | `200`, `404` if the `id` does not exist or the data is not acceptable |
| Delete a driver | `DELETE` | `/piloti/{id}` | `200`, `404` if the `id` does not exist |

> The team filter was a **bonus** in exercise 8.1: here it is **required**, because
> it is a great example of a rule that belongs in the Service.

### Step 1 — Create the folder and the project

Remember: your work goes inside `MyExercises`, not inside `Exercises`.
Open a terminal in the main folder of the repository and type:

```bash
cd MyExercises
mkdir 9.1-CrudPilotiCSR
cd 9.1-CrudPilotiCSR
dotnet new webapi -f net8.0
```

As always: no `-n`, and `-f net8.0` to use .NET 8.

Then open `9.1-CrudPilotiCSR.csproj` and check the package versions, as in
exercise 8.1:

| Package | Right version for .NET 8 |
|---|---|
| `Microsoft.AspNetCore.OpenApi` | must start with **8** (e.g. `8.0.11`) |
| `Swashbuckle.AspNetCore` | must start with **6** (e.g. `6.9.0`) |

### Step 2 — Copy the `Pilota` class

The `Pilota` class does not change: copy your **`Pilota.cs`** from exercise 8.1
and put it next to `Program.cs`.

### Step 3 — Create the three folders

Inside the project create the folders:

```
9.1-CrudPilotiCSR/
├── Endpoints/
├── Services/
├── Repositories/
├── Pilota.cs
└── Program.cs
```

It is best to write the layers **from the bottom up**: first the Repository (which
depends on nobody), then the Service, and finally the Controller.

### Step 4 — The Repository: `Repositories/PilotaRepository.cs`

Create the `public class PilotaRepository` class. It is the **only** class that sees
the drivers dictionary.

1. Move the **`Dictionary<int, Pilota>`** here from the old `Program.cs` and make it
   **`private`**: no other layer must be able to touch it.
2. Fill it right away with the two starting drivers (Leclerc and Norris), as in 8.1.
3. Write these methods:

| Method | Returns |
|---|---|
| `GetPiloti()` | `IEnumerable<Pilota>`: all the drivers |
| `GetPilota(int id)` | `Pilota?`: the driver, or `null` if it does not exist |
| `Create(Pilota pilota)` | `bool`: `false` if the `id` is already there, otherwise adds it and returns `true` |
| `Update(Pilota pilota)` | `bool`: `false` if the `id` is not there, otherwise replaces it and returns `true` |
| `Delete(int id)` | `bool`: `false` if the `id` is not there, otherwise deletes it and returns `true` |

Model: `PersonRepository.cs` from exercise 9.

### Step 5 — The Service: `Services/PilotaService.cs`

Create the `public class PilotaService` class. Inside it create the Repository:

```csharp
private PilotaRepository pilotaRepository = new PilotaRepository();
```

Then write the **same methods** as the Repository (`GetPiloti`, `GetPilota`, `Create`,
`Update`, `Delete`), which call the Repository, plus the one for the team:

- **`GetPilotiDiScuderia(string nome)`** — takes all the drivers from the Repository and
  keeps only those of the given team. The comparison must ignore upper and lower case:
  `"ferrari"` and `"Ferrari"` are the same team.

The **rules** all go here. Write a **`private bool IsValid(Pilota pilota)`** method and
use it in both `Create` and `Update`: if the data is not valid, return `false`
**without even calling** the Repository.

A driver is valid if:

1. the **`Nome`** is not empty (hint: `string.IsNullOrWhiteSpace`);
2. the **`Scuderia`** is not empty;
3. the race **`Numero`** is between **1** and **99**.

Then there is a rule that applies **only to `Create`**:

4. the race **`Numero`** must not be already used by another driver (it was the second
   bonus of 8.1).

Model: `PersonService.cs` from exercise 9 (look at `IsValid` and `GetAdults`).

### Step 6 — The Controller: `Endpoints/PilotaEndpoints.cs`

Create the **`public static class PilotaEndpoints`** class. Inside it:

1. A **`private static PilotaService pilotaService = new PilotaService();`** field.
   It is `static` because there must be **only one** Service (and therefore only one
   Repository) for the whole life of the API: otherwise the data would be lost.
2. An extension method **`public static void MapPilotiEndpoints(this IEndpointRouteBuilder app)`**
   holding the six `app.MapGet`, `app.MapPost`, `app.MapPut`, `app.MapDelete`.

Each endpoint does **only two things**: it calls the Service and turns the result into
an HTTP response. For example, if `pilotaService.Create(pilota)` returns `true`
answer `Results.Ok()`, otherwise `Results.BadRequest()`.

Model: `PersonEndpoints.cs` from exercise 9.

### Step 7 — Clean up `Program.cs`

Now `Program.cs` must stay **very short**: no dictionary, no endpoints.
Only the builder with Swagger, `builder.Build()`, the call

```csharp
app.MapPilotiEndpoints();
```

turning Swagger on and `app.Run()`. Compare it with the `Program.cs` of exercise 9:
they must be almost the same.

### Step 8 — The `.http` file

Start the API with `dotnet run` and, from a **second terminal** in the project folder,
generate the requests file as in exercise 8.1:

```bash
httpgenerator http://localhost:<porta>/swagger/v1/swagger.json --output-type OneFile --output .
```

Then fill in the requests with real data.

### What to try

Look at the **response code** each time:

1. `GET /piloti`: the two starting drivers are there.
2. `GET /piloti/1`: `200` with Leclerc. `GET /piloti/99`: `404`.
3. `POST /piloti` with this body:
   ```json
   { "id": 3, "nome": "Lewis Hamilton", "scuderia": "Ferrari", "numero": 44 }
   ```
   Then repeat the same request: now it must answer `400`.
4. `GET /piloti/scuderia/ferrari`: Leclerc and Hamilton.
5. `POST /piloti` with `"nome": ""`: `400`. With `"numero": 150`: `400`.
   With `"numero": 16` (already Leclerc's): `400`.
6. `PUT /piloti` with `id` 3 and a different number, then read it again with `GET /piloti/3`.
7. `PUT /piloti` with `id` 99: `404`.
8. `DELETE /piloti/3`, then try to delete it a second time: `404`.

If you did the refactoring right, points 1, 2, 3, 6, 7 and 8 answer **exactly** as in
exercise 8.1.

### What to hand in

The `MyExercises/9.1-CrudPilotiCSR` folder with:

- `Pilota.cs`
- `Repositories/PilotaRepository.cs`
- `Services/PilotaService.cs`
- `Endpoints/PilotaEndpoints.cs`
- the cleaned-up `Program.cs`
- the `.http` file with the requests filled in with real data

### Questions to answer

- The "does the `id` already exist?" check lives in the **Repository**, while "is the
  race number already used?" lives in the **Service**. Why? What is the difference
  between a **data constraint** and a **program rule**?
- If tomorrow the drivers were stored in a database instead of a `Dictionary`, which
  files would you have to change? And which would stay identical?
- What would happen if in the Controller you removed `static` from the `pilotaService`
  field?
- With `PUT` and invalid data the API answers `404`, as if the driver did not exist.
  Does that seem right to you? How could you tell the two cases apart?

### Bonus (for those who finish early)

- **Ranking by number.** Add `GET /piloti/ordinati`, which returns the drivers sorted
  by race `Numero`. Sorting is a rule: in which layer should it be written?
- **Unique number in `PUT` too.** When changing a driver, answer with an error if the
  new `Numero` is already used by **another** driver (careful: not by the driver itself!).
