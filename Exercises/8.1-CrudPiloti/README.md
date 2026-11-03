# Esercitazione 8.1 — API CRUD di piloti

> **Questa esercitazione la fai da solo.** In questa cartella c'è solo la traccia:
> il progetto lo crei tu e il codice lo scrivi tu. Come modello usa l'esercitazione 8
> (CrudPersone): la struttura è quasi identica.

## Che cosa costruiamo

Una **API web** che gestisce un elenco di **piloti di F1**. Come nell'esercitazione 8,
l'API sa fare le quattro operazioni **CRUD** e i dati restano in memoria in un
`Dictionary`.

Ogni pilota ha quattro dati:

| Proprietà | Tipo | Esempio |
|---|---|---|
| `Id` | `int` | `1` |
| `Nome` | `string?` | `"Charles Leclerc"` |
| `Scuderia` | `string?` | `"Ferrari"` |
| `Numero` | `int` | `16` (il numero di gara) |

L'API deve avere questi cinque endpoint:

| Operazione | Verbo HTTP | Indirizzo | Risposte |
|---|---|---|---|
| Leggi tutti i piloti | `GET` | `/piloti` | `200` con l'elenco (anche vuoto: `[]`) |
| Leggi un pilota | `GET` | `/piloti/{id}` | `200` con il pilota, `404` se non esiste |
| Aggiungi un pilota | `POST` | `/piloti` | `200`, `400` se l'`id` esiste già |
| Modifica un pilota | `PUT` | `/piloti` | `200`, `404` se l'`id` non esiste |
| Cancella un pilota | `DELETE` | `/piloti/{id}` | `200`, `404` se l'`id` non esiste |

## Che cosa cambia rispetto all'esercitazione 8

Le differenze sono solo tre, ma sono importanti. Leggile bene prima di cominciare.

1. **L'indirizzo è sempre `/piloti`.** Nell'esercitazione 8 ogni operazione aveva un
   nome diverso (`/getPeople`, `/create`, `/update`, `/delete`). Qui no: l'indirizzo
   dice solo **quali dati** (i piloti), mentre **che cosa fare** lo dice il verbo HTTP
   (`GET`, `POST`, `PUT`, `DELETE`). È così che si scrivono le API vere.
2. **Il pilota che non esiste risponde `404`.** Nell'esercitazione 8,
   `GET /getPerson/99` rispondeva `200 OK` con `null`. Qui `GET /piloti/99` deve
   rispondere **`404 Not Found`**, come fanno già `PUT` e `DELETE`. Per farlo, la
   lambda deve restituire `Results.Ok(pilota)` quando lo trova e `Results.NotFound()`
   quando non lo trova.
3. **La classe si chiama `Pilota`** e la chiave si scrive `Id` con la maiuscola, come
   vuole la convenzione C#. Nel JSON diventa comunque `"id"`.

## Passo 1 — Crea la cartella e il progetto

Ricorda: il tuo lavoro va dentro `MyExercises`, non dentro `Exercises`.
Apri un terminale nella cartella principale del repository e scrivi:

```bash
cd MyExercises
mkdir 8.1-CrudPiloti
cd 8.1-CrudPiloti
dotnet new webapi -f net8.0
```

Come nell'esercitazione 8: niente `-n`, e `-f net8.0` per usare .NET 8.

## Passo 2 — Sistema i pacchetti

Apri il file `8.1-CrudPiloti.csproj` e controlla le righe `PackageReference`,
esattamente come al passo 6 dell'esercitazione 8:

| Pacchetto | Versione giusta per .NET 8 |
|---|---|
| `Microsoft.AspNetCore.OpenApi` | deve iniziare con **8** (es. `8.0.11`) |
| `Swashbuckle.AspNetCore` | deve iniziare con **6** (es. `6.9.0`) |

Se i numeri sono diversi, installa la versione giusta con `--version`:

```bash
dotnet add package Microsoft.AspNetCore.OpenApi --version 8.0.11
dotnet add package Swashbuckle.AspNetCore --version 6.9.0
```

## Passo 3 — Scrivi la classe `Pilota`

Crea il file **`Pilota.cs`** accanto a `Program.cs`. Dentro scrivi una classe
`public class Pilota` con le quattro proprietà della tabella qui sopra, tutte
pubbliche con `get` e `set`.

Modello: `Person.cs` dell'esercitazione 8.

## Passo 4 — Scrivi `Program.cs`

Cancella tutto il contenuto del `Program.cs` creato da `dotnet new` e scrivi il tuo,
prendendo come modello il `Program.cs` dell'esercitazione 8. Nell'ordine:

1. Il **builder** con i due servizi di Swagger (`AddEndpointsApiExplorer` e `AddSwaggerGen`).
2. `builder.Build()` e l'attivazione di Swagger dentro `if (app.Environment.IsDevelopment())`.
3. Il dizionario **`Dictionary<int, Pilota> piloti`**.
4. **Riempi subito il dizionario con due piloti**, così la prima `GET` restituisce
   già qualcosa. Per esempio:
   ```csharp
   piloti.Add(1, new Pilota { Id = 1, Nome = "Charles Leclerc", Scuderia = "Ferrari", Numero = 16 });
   piloti.Add(2, new Pilota { Id = 2, Nome = "Lando Norris", Scuderia = "McLaren", Numero = 4 });
   ```
5. I **cinque endpoint** della tabella, con `MapGet`, `MapPost`, `MapPut` e `MapDelete`.
6. `app.Run()` alla fine.

Le risposte che ti servono sono tre:

| Codice | Si scrive | Quando |
|---|---|---|
| `200 OK` | `Results.Ok()` oppure `Results.Ok(pilota)` | tutto bene |
| `400 Bad Request` | `Results.BadRequest("messaggio")` | la richiesta non va bene |
| `404 Not Found` | `Results.NotFound()` | il pilota non esiste |

> **Consiglio:** non scrivere tutto in una volta. Scrivi un endpoint, avvia con
> `dotnet run`, provalo su Swagger (`http://localhost:<porta>/swagger`), poi passa al
> successivo.

## Passo 5 — Genera il file `.http`

Quando tutti e cinque gli endpoint funzionano, genera il file con le richieste.
Installa lo strumento, se non l'hai già fatto (basta una volta sola):

```bash
dotnet tool install --global httpgenerator
```

Poi, **con l'API avviata**, apri un **secondo terminale** nella cartella del progetto
e scrivi (al posto di `<porta>` metti il numero che vedi nel terminale dell'API,
nella riga `Now listening on: http://localhost:...`):

```bash
httpgenerator http://localhost:<porta>/swagger/v1/swagger.json --output-type OneFile --output .
```

Nella cartella compare il file `Requests.http`. I valori dentro sono di esempio
(`"id": 0`, `"nome": "example"`...): cambiali con dati veri e, nel corpo di `POST` e
`PUT`, aggiungi anche `"numero"`. Poi clicca **Send Request** sopra ogni richiesta.

## Che cosa provare

Usa il file `Requests.http` e guarda ogni volta il **codice di risposta**:

1. `GET /piloti`: ci sono i due piloti di partenza.
2. `GET /piloti/1`: risponde `200` con Leclerc. `GET /piloti/99`: risponde `404`.
3. `POST /piloti` con questo corpo:
   ```json
   { "id": 3, "nome": "Lewis Hamilton", "scuderia": "Ferrari", "numero": 44 }
   ```
   Poi ripeti la stessa richiesta: adesso deve rispondere `400`.
4. `PUT /piloti` con `id` 3 e un numero diverso, poi rileggilo con `GET /piloti/3`.
5. `PUT /piloti` con `id` 99: deve rispondere `404`.
6. `DELETE /piloti/3`, poi prova a cancellarlo una seconda volta: `404`.

## Che cosa consegnare

La cartella `MyExercises/8.1-CrudPiloti` con:

- `Pilota.cs`
- `Program.cs` con i cinque endpoint
- `Requests.http` con le richieste compilate con dati veri

## Bonus (per chi finisce prima)

- **Filtro per scuderia.** Aggiungi `GET /piloti/scuderia/{nome}`, che restituisce solo
  i piloti di quella scuderia. Suggerimento: `piloti.Values.Where(...)`.
- **Numero unico.** Nella `POST`, rispondi `400` anche quando il `Numero` di gara è
  già usato da un altro pilota.

Dopo aver aggiunto un endpoint, **rilancia `httpgenerator`**: il file `.http` si
aggiorna da solo.

---

## English version

# Exercise 8.1 — Drivers CRUD API

> **You do this exercise on your own.** This folder only holds the outline:
> you create the project and you write the code. Use exercise 8 (CrudPersone)
> as a model: the structure is almost identical.

### What we build

A **web API** that manages a list of **F1 drivers**. As in exercise 8, the API can
do the four **CRUD** operations and the data stays in memory in a `Dictionary`.

Each driver has four pieces of data:

| Property | Type | Example |
|---|---|---|
| `Id` | `int` | `1` |
| `Nome` | `string?` | `"Charles Leclerc"` |
| `Scuderia` | `string?` | `"Ferrari"` |
| `Numero` | `int` | `16` (the race number) |

The API must have these five endpoints:

| Operation | HTTP verb | Address | Responses |
|---|---|---|---|
| Read all drivers | `GET` | `/piloti` | `200` with the list (even empty: `[]`) |
| Read one driver | `GET` | `/piloti/{id}` | `200` with the driver, `404` if it does not exist |
| Add a driver | `POST` | `/piloti` | `200`, `400` if the `id` already exists |
| Change a driver | `PUT` | `/piloti` | `200`, `404` if the `id` does not exist |
| Delete a driver | `DELETE` | `/piloti/{id}` | `200`, `404` if the `id` does not exist |

### What changes compared to exercise 8

There are only three differences, but they matter. Read them carefully before you start.

1. **The address is always `/piloti`.** In exercise 8 each operation had a different
   name (`/getPeople`, `/create`, `/update`, `/delete`). Not here: the address only
   says **which data** (the drivers), while **what to do** is said by the HTTP verb
   (`GET`, `POST`, `PUT`, `DELETE`). This is how real APIs are written.
2. **A driver that does not exist answers `404`.** In exercise 8,
   `GET /getPerson/99` answered `200 OK` with `null`. Here `GET /piloti/99` must
   answer **`404 Not Found`**, as `PUT` and `DELETE` already do. To do that, the
   lambda must return `Results.Ok(pilota)` when it finds it and `Results.NotFound()`
   when it does not.
3. **The class is called `Pilota`** and the key is written `Id` with a capital letter,
   as the C# convention wants. In JSON it still becomes `"id"`.

### Step 1 — Create the folder and the project

Remember: your work goes inside `MyExercises`, not inside `Exercises`.
Open a terminal in the main folder of the repository and type:

```bash
cd MyExercises
mkdir 8.1-CrudPiloti
cd 8.1-CrudPiloti
dotnet new webapi -f net8.0
```

As in exercise 8: no `-n`, and `-f net8.0` to use .NET 8.

### Step 2 — Fix the packages

Open the `8.1-CrudPiloti.csproj` file and check the `PackageReference` lines,
exactly as in step 6 of exercise 8:

| Package | Right version for .NET 8 |
|---|---|
| `Microsoft.AspNetCore.OpenApi` | must start with **8** (e.g. `8.0.11`) |
| `Swashbuckle.AspNetCore` | must start with **6** (e.g. `6.9.0`) |

If the numbers are different, install the right version with `--version`:

```bash
dotnet add package Microsoft.AspNetCore.OpenApi --version 8.0.11
dotnet add package Swashbuckle.AspNetCore --version 6.9.0
```

### Step 3 — Write the `Pilota` class

Create the **`Pilota.cs`** file next to `Program.cs`. Inside it write a
`public class Pilota` class with the four properties of the table above, all
public with `get` and `set`.

Model: `Person.cs` from exercise 8.

### Step 4 — Write `Program.cs`

Delete all the content of the `Program.cs` created by `dotnet new` and write your own,
using the `Program.cs` of exercise 8 as a model. In this order:

1. The **builder** with the two Swagger services (`AddEndpointsApiExplorer` and `AddSwaggerGen`).
2. `builder.Build()` and turning Swagger on inside `if (app.Environment.IsDevelopment())`.
3. The **`Dictionary<int, Pilota> piloti`** dictionary.
4. **Fill the dictionary right away with two drivers**, so the first `GET` already
   returns something. For example:
   ```csharp
   piloti.Add(1, new Pilota { Id = 1, Nome = "Charles Leclerc", Scuderia = "Ferrari", Numero = 16 });
   piloti.Add(2, new Pilota { Id = 2, Nome = "Lando Norris", Scuderia = "McLaren", Numero = 4 });
   ```
5. The **five endpoints** of the table, with `MapGet`, `MapPost`, `MapPut` and `MapDelete`.
6. `app.Run()` at the end.

You need three responses:

| Code | Written as | When |
|---|---|---|
| `200 OK` | `Results.Ok()` or `Results.Ok(pilota)` | all good |
| `400 Bad Request` | `Results.BadRequest("message")` | the request is not acceptable |
| `404 Not Found` | `Results.NotFound()` | the driver does not exist |

> **Tip:** do not write everything at once. Write one endpoint, start with
> `dotnet run`, try it on Swagger (`http://localhost:<port>/swagger`), then move on
> to the next one.

### Step 5 — Generate the `.http` file

When all five endpoints work, generate the file with the requests.
Install the tool, if you have not done it yet (once is enough):

```bash
dotnet tool install --global httpgenerator
```

Then, **with the API running**, open a **second terminal** in the project folder
and type (instead of `<porta>` put the number you see in the API terminal, in the
line `Now listening on: http://localhost:...`):

```bash
httpgenerator http://localhost:<porta>/swagger/v1/swagger.json --output-type OneFile --output .
```

The `Requests.http` file appears in the folder. The values inside are examples
(`"id": 0`, `"nome": "example"`...): change them to real data and, in the body of
`POST` and `PUT`, also add `"numero"`. Then click **Send Request** above each request.

### What to try

Use the `Requests.http` file and look at the **response code** each time:

1. `GET /piloti`: the two starting drivers are there.
2. `GET /piloti/1`: answers `200` with Leclerc. `GET /piloti/99`: answers `404`.
3. `POST /piloti` with this body:
   ```json
   { "id": 3, "nome": "Lewis Hamilton", "scuderia": "Ferrari", "numero": 44 }
   ```
   Then repeat the same request: now it must answer `400`.
4. `PUT /piloti` with `id` 3 and a different number, then read it again with `GET /piloti/3`.
5. `PUT /piloti` with `id` 99: it must answer `404`.
6. `DELETE /piloti/3`, then try to delete it a second time: `404`.

### What to hand in

The `MyExercises/8.1-CrudPiloti` folder with:

- `Pilota.cs`
- `Program.cs` with the five endpoints
- `Requests.http` with the requests filled in with real data

### Bonus (for those who finish early)

- **Filter by team.** Add `GET /piloti/scuderia/{nome}`, which returns only the
  drivers of that team. Hint: `piloti.Values.Where(...)`.
- **Unique number.** In the `POST`, answer `400` also when the race `Numero` is
  already used by another driver.

After adding an endpoint, **run `httpgenerator` again**: the `.http` file updates by itself.
