# Esercitazione 8 — API CRUD di persone

> **Questa esercitazione la costruisci tu.** In questa cartella c'è solo la traccia:
> il progetto lo crei con `dotnet new` e il codice lo copi da
> [`Resources/Esercitazione8`](../../Resources/Esercitazione8).

## Che cosa costruiamo

Una piccola **API web** che gestisce un elenco di persone. Ogni persona ha un `id`,
un nome e un'età. L'API sa fare le quattro operazioni base sui dati, chiamate
**CRUD**:

| Operazione | Verbo HTTP | Indirizzo |
|---|---|---|
| **C**reate — aggiungi una persona | `POST` | `/create` |
| **R**ead — leggi tutte le persone | `GET` | `/getPeople` |
| **R**ead — leggi una persona | `GET` | `/getPerson/{id}` |
| **U**pdate — modifica una persona | `PUT` | `/update` |
| **D**elete — cancella una persona | `DELETE` | `/delete/{id}` |

I dati restano in memoria: quando fermi l'API, tutto quello che hai inserito si perde.

## Passo 1 — Crea la cartella del progetto

Ricorda: il tuo lavoro va dentro `MyExercises`, non dentro `Exercises`.
Apri un terminale nella cartella principale del repository e scrivi:

```bash
cd MyExercises
mkdir 8-CrudPersone
cd 8-CrudPersone
```

## Passo 2 — Crea la web API

Adesso sei **dentro** la cartella `8-CrudPersone`. Scrivi:

```bash
dotnet new webapi -f net8.0
```

Nota due cose:

- **Non serve `-n`.** Se non dai un nome, `dotnet new` crea i file direttamente
  nella cartella in cui ti trovi e dà al progetto **lo stesso nome della cartella**:
  otterrai `8-CrudPersone.csproj`. Se invece scrivessi `-n NomeProgetto`, verrebbe
  creata un'altra cartella dentro questa.
- **`-f net8.0`** sceglie .NET 8. È importante: il codice che copierai usa Swagger
  nella versione per .NET 8, e senza questa opzione non compilerebbe.

## Passo 3 — Copia i due file

Da [`Resources/Esercitazione8`](../../Resources/Esercitazione8) copia nella tua
cartella `MyExercises/8-CrudPersone`:

- **`Program.cs`** — sostituisce quello creato da `dotnet new`: quando ti chiede se
  sovrascriverlo, rispondi di sì.
- **`Person.cs`** — è un file nuovo, mettilo accanto a `Program.cs`.

Leggi i commenti dentro i due file: spiegano riga per riga come funziona l'API.

## Passo 4 — Avvia e prova

```bash
dotnet run
```

Nel terminale compare una riga simile a questa (il numero della porta cambia da
computer a computer):

```
Now listening on: http://localhost:5123
```

Apri il browser su quell'indirizzo aggiungendo `/swagger` in fondo, per esempio
`http://localhost:5123/swagger`. Vedrai l'elenco degli endpoint: aprine uno, premi
**Try it out** e poi **Execute** per provarlo.

Per fermare l'API torna nel terminale e premi `Ctrl+C`.

## Che cosa provare

Segui quest'ordine e guarda ogni volta il **codice di risposta** (200, 400, 404...):

1. `GET /getPeople` — all'inizio l'elenco è vuoto: `[]`.
2. `POST /create` con questo corpo:
   ```json
   { "id": 1, "name": "Mario Rossi", "age": 30 }
   ```
3. Ripeti la stessa `POST`: che codice ottieni adesso? Perché?
4. `GET /getPeople` e `GET /getPerson/1`: la persona c'è.
5. `PUT /update` con lo stesso `id` e un'età diversa, poi rileggila.
6. `DELETE /delete/1`, poi prova a cancellarla una seconda volta.

## Domande a cui rispondere

- Che cosa risponde `GET /getPerson/99`, cioè una persona che non esiste? E
  `PUT /update` con un `id` che non esiste? Le due risposte sono coerenti tra loro?
- Che cosa succede con `GET /getPerson/abc`? Chi ha deciso la risposta: il nostro
  codice o ASP.NET Core?
- Ferma l'API con `Ctrl+C` e riavviala: dove sono finite le persone che avevi inserito?

---

## English version

# Exercise 8 — People CRUD API

> **You build this exercise yourself.** This folder only holds the outline:
> you create the project with `dotnet new` and copy the code from
> [`Resources/Esercitazione8`](../../Resources/Esercitazione8).

### What we build

A small **web API** that manages a list of people. Each person has an `id`,
a name and an age. The API can do the four basic operations on data, called
**CRUD**:

| Operation | HTTP verb | Address |
|---|---|---|
| **C**reate — add a person | `POST` | `/create` |
| **R**ead — read all people | `GET` | `/getPeople` |
| **R**ead — read one person | `GET` | `/getPerson/{id}` |
| **U**pdate — change a person | `PUT` | `/update` |
| **D**elete — delete a person | `DELETE` | `/delete/{id}` |

The data stays in memory: when you stop the API, everything you inserted is lost.

### Step 1 — Create the project folder

Remember: your work goes inside `MyExercises`, not inside `Exercises`.
Open a terminal in the main folder of the repository and type:

```bash
cd MyExercises
mkdir 8-CrudPersone
cd 8-CrudPersone
```

### Step 2 — Create the web API

Now you are **inside** the `8-CrudPersone` folder. Type:

```bash
dotnet new webapi -f net8.0
```

Notice two things:

- **You do not need `-n`.** If you give no name, `dotnet new` creates the files
  directly in the folder you are in and gives the project **the same name as the
  folder**: you will get `8-CrudPersone.csproj`. If you wrote `-n ProjectName`
  instead, another folder would be created inside this one.
- **`-f net8.0`** chooses .NET 8. It matters: the code you will copy uses Swagger
  in its .NET 8 version, and without this option it would not compile.

### Step 3 — Copy the two files

From [`Resources/Esercitazione8`](../../Resources/Esercitazione8) copy into your
`MyExercises/8-CrudPersone` folder:

- **`Program.cs`** — it replaces the one created by `dotnet new`: when asked whether
  to overwrite it, answer yes.
- **`Person.cs`** — it is a new file, put it next to `Program.cs`.

Read the comments inside the two files: they explain line by line how the API works.

### Step 4 — Run and try it

```bash
dotnet run
```

A line like this one appears in the terminal (the port number changes from
computer to computer):

```
Now listening on: http://localhost:5123
```

Open the browser at that address adding `/swagger` at the end, for example
`http://localhost:5123/swagger`. You will see the list of endpoints: open one, press
**Try it out** and then **Execute** to try it.

To stop the API go back to the terminal and press `Ctrl+C`.

### What to try

Follow this order and look at the **response code** each time (200, 400, 404...):

1. `GET /getPeople` — at the start the list is empty: `[]`.
2. `POST /create` with this body:
   ```json
   { "id": 1, "name": "Mario Rossi", "age": 30 }
   ```
3. Repeat the same `POST`: what code do you get now? Why?
4. `GET /getPeople` and `GET /getPerson/1`: the person is there.
5. `PUT /update` with the same `id` and a different age, then read it again.
6. `DELETE /delete/1`, then try to delete it a second time.

### Questions to answer

- What does `GET /getPerson/99` answer, that is a person that does not exist? And
  `PUT /update` with an `id` that does not exist? Are the two answers consistent
  with each other?
- What happens with `GET /getPerson/abc`? Who decided the answer: our code or
  ASP.NET Core?
- Stop the API with `Ctrl+C` and start it again: where did the people you inserted go?
