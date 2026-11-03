# Mini guida al modello CSR

Questa guida accompagna l'[esercitazione 9](../Exercises/9-CrudPersoneCSR) e spiega
le cose nuove che ci trovi dentro. Il codice dell'API è lo stesso dell'esercitazione 8:
cambia solo il modo in cui è organizzato.

## Il modello CSR in breve

**CSR** sta per **Controller – Service – Repository**: il codice viene diviso in tre
livelli, ognuno con un solo compito.

| Livello | Cartella | Compito | Non deve... |
|---|---|---|---|
| **Controller** | `Endpoints` | riceve la richiesta HTTP e sceglie la risposta (200, 400, 404) | contenere regole o toccare i dati |
| **Service** | `Services` | applica le regole del programma (es. "l'età non può essere negativa") | sapere cos'è HTTP |
| **Repository** | `Repositories` | legge e scrive i dati | contenere regole o sapere cos'è HTTP |

Una richiesta passa sempre per tutti e tre, nello stesso ordine:

```
client  →  Controller  →  Service  →  Repository  →  dati
```

**La regola d'oro:** ogni livello parla **solo** con quello subito sotto. Così, se
domani i dati passano da un dizionario a un database, si riscrive solo il Repository:
Controller e Service non se ne accorgono nemmeno.

## Un file per classe, una cartella per livello

Nell'esercitazione 8 tutto stava in `Program.cs`. Qui ogni classe ha il suo file, e
ogni file sta nella cartella del suo livello:

```
9-CrudPersoneCSR/
├── Program.cs                     avvia l'applicazione
├── Person.cs                      i dati di una persona
├── Endpoints/PersonEndpoints.cs   Controller
├── Services/PersonService.cs       Service
└── Repositories/PersonRepository.cs  Repository
```

Le cartelle servono solo a **noi** per orientarci: per il compilatore tutti i file
`.cs` del progetto fanno parte dello stesso programma, ovunque si trovino.

## `IEnumerable<Person>`: "un elenco da scorrere"

Il metodo che legge tutte le persone non restituisce un array né una `List`, ma un
`IEnumerable<Person>`:

```csharp
public IEnumerable<Person> GetPeople()
```

`IEnumerable` promette una cosa sola: **l'elenco si può scorrere** con un `foreach`
(o con LINQ). Non dice com'è fatto dentro e non permette di aggiungere o togliere
elementi.

| Tipo | Si può scorrere? | Si può fare `Add` / `Remove`? | Si sa com'è fatto dentro? |
|---|---|---|---|
| `List<Person>` | sì | sì | sì, è una lista |
| `Person[]` | sì | no | sì, è un array |
| `IEnumerable<Person>` | sì | no | **no** |

Perché è utile nel CSR? Oggi il Repository restituisce un array
(`dict.Values.ToArray()`); domani potrebbe restituire i risultati di un database.
Finché il tipo resta `IEnumerable<Person>`, Service e Controller non devono cambiare
nemmeno una riga.

Un array, una `List` e i `Values` di un dizionario sono tutti `IEnumerable`: per questo
si possono restituire senza conversioni.

## `bool`: "è andata bene?"

Il Repository non conosce HTTP, quindi non può rispondere `Results.NotFound()`.
Risponde semplicemente `true` (fatto) o `false` (non è stato possibile):

```csharp
// Repository: dice solo com'è andata
// EN: Repository: only says how it went
public bool Delete(int id)
{
    if (!dict.ContainsKey(id))
        return false;
    dict.Remove(id);
    return true;
}
```

È il Controller a tradurre quel `bool` in una risposta HTTP:

```csharp
// Controller: sceglie il codice HTTP
// EN: Controller: picks the HTTP code
bool result = personService.Delete(id);
if (result == true)
    return Results.Ok();        // 200
else
    return Results.NotFound();  // 404
```

## `Person?`: "forse c'è, forse no"

```csharp
public Person? GetPerson(int id)
```

Il punto di domanda dice che il metodo può restituire **una persona oppure `null`**,
cioè "non l'ho trovata". Chi lo chiama deve ricordarsi che il risultato può essere vuoto.

## `private`: i dati li tocca solo il Repository

```csharp
private Dictionary<int, Person> dict = new Dictionary<int, Person>();
```

`private` vuol dire che il dizionario si può usare **solo dentro** `PersonRepository`.
Se il Controller provasse a scrivere `dict.Add(...)`, il programma non compilerebbe.
È il compilatore stesso a far rispettare la regola d'oro del CSR.

## `static` e `this`: come si spostano gli endpoint fuori da `Program.cs`

In `Program.cs` c'è una sola riga per registrare tutti gli endpoint:

```csharp
app.MapAPIEndpoints();
```

`MapAPIEndpoints` non è un metodo di ASP.NET Core: l'abbiamo scritto noi in
`PersonEndpoints.cs`. Funziona grazie a due parole chiave:

```csharp
public static class PersonEndpoints
{
    public static void MapAPIEndpoints(this IEndpointRouteBuilder app)
```

- **`static`** — la classe non si istanzia con `new`: esiste già, una volta sola.
- **`this`** davanti al primo parametro — trasforma il metodo in un **extension
  method**: lo si può chiamare come se fosse un metodo di `app`. Scrivere
  `app.MapAPIEndpoints()` è come scrivere `PersonEndpoints.MapAPIEndpoints(app)`.

Anche il Service, dentro il Controller, è `static`:

```csharp
private static PersonService personService = new PersonService();
```

Così ne esiste **uno solo** per tutta l'applicazione, e con lui un solo Repository e un
solo dizionario. Se ne creassimo uno nuovo a ogni richiesta, ogni volta partiremmo da
un dizionario vuoto e le persone inserite sparirebbero.

---

## English version

# CSR pattern mini guide

This guide goes with [exercise 9](../Exercises/9-CrudPersoneCSR) and explains the new
things you find in it. The API code is the same as in exercise 8: only the way it is
organized changes.

### The CSR pattern in short

**CSR** stands for **Controller – Service – Repository**: the code is split into three
layers, each with a single job.

| Layer | Folder | Job | Must not... |
|---|---|---|---|
| **Controller** | `Endpoints` | receives the HTTP request and picks the response (200, 400, 404) | hold rules or touch the data |
| **Service** | `Services` | applies the program rules (e.g. "the age cannot be negative") | know what HTTP is |
| **Repository** | `Repositories` | reads and writes the data | hold rules or know what HTTP is |

A request always goes through all three, in the same order:

```
client  →  Controller  →  Service  →  Repository  →  data
```

**The golden rule:** each layer talks **only** to the one right below it. This way, if
tomorrow the data moves from a dictionary to a database, only the Repository is
rewritten: Controller and Service do not even notice.

### One file per class, one folder per layer

In exercise 8 everything was in `Program.cs`. Here each class has its own file, and
each file sits in the folder of its layer:

```
9-CrudPersoneCSR/
├── Program.cs                     starts the application
├── Person.cs                      the data of a person
├── Endpoints/PersonEndpoints.cs   Controller
├── Services/PersonService.cs       Service
└── Repositories/PersonRepository.cs  Repository
```

The folders are only there for **us** to find our way: for the compiler all the `.cs`
files of the project are part of the same program, wherever they are.

### `IEnumerable<Person>`: "a list to go through"

The method that reads all the people returns neither an array nor a `List`, but an
`IEnumerable<Person>`:

```csharp
public IEnumerable<Person> GetPeople()
```

`IEnumerable` promises one thing only: **the list can be gone through** with a
`foreach` (or with LINQ). It does not say how it is made inside and does not allow
adding or removing elements.

| Type | Can be gone through? | Can do `Add` / `Remove`? | Do we know how it is made inside? |
|---|---|---|---|
| `List<Person>` | yes | yes | yes, it is a list |
| `Person[]` | yes | no | yes, it is an array |
| `IEnumerable<Person>` | yes | no | **no** |

Why is it useful in CSR? Today the Repository returns an array (`dict.Values.ToArray()`);
tomorrow it could return the results of a database. As long as the type stays
`IEnumerable<Person>`, Service and Controller do not have to change a single line.

An array, a `List` and the `Values` of a dictionary are all `IEnumerable`: that is why
they can be returned without conversions.

### `bool`: "did it work?"

The Repository does not know HTTP, so it cannot answer `Results.NotFound()`.
It simply answers `true` (done) or `false` (it was not possible):

```csharp
// Repository: dice solo com'è andata
// EN: Repository: only says how it went
public bool Delete(int id)
{
    if (!dict.ContainsKey(id))
        return false;
    dict.Remove(id);
    return true;
}
```

It is the Controller that translates that `bool` into an HTTP response:

```csharp
// Controller: sceglie il codice HTTP
// EN: Controller: picks the HTTP code
bool result = personService.Delete(id);
if (result == true)
    return Results.Ok();        // 200
else
    return Results.NotFound();  // 404
```

### `Person?`: "maybe it is there, maybe not"

```csharp
public Person? GetPerson(int id)
```

The question mark says that the method can return **a person or `null`**, that is
"I did not find it". Whoever calls it must remember that the result can be empty.

### `private`: only the Repository touches the data

```csharp
private Dictionary<int, Person> dict = new Dictionary<int, Person>();
```

`private` means the dictionary can be used **only inside** `PersonRepository`.
If the Controller tried to write `dict.Add(...)`, the program would not compile.
The compiler itself enforces the golden rule of CSR.

### `static` and `this`: how the endpoints move out of `Program.cs`

In `Program.cs` there is a single line to register all the endpoints:

```csharp
app.MapAPIEndpoints();
```

`MapAPIEndpoints` is not an ASP.NET Core method: we wrote it ourselves in
`PersonEndpoints.cs`. It works thanks to two keywords:

```csharp
public static class PersonEndpoints
{
    public static void MapAPIEndpoints(this IEndpointRouteBuilder app)
```

- **`static`** — the class is not instantiated with `new`: it already exists, only once.
- **`this`** in front of the first parameter — turns the method into an **extension
  method**: it can be called as if it were a method of `app`. Writing
  `app.MapAPIEndpoints()` is like writing `PersonEndpoints.MapAPIEndpoints(app)`.

The Service inside the Controller is `static` too:

```csharp
private static PersonService personService = new PersonService();
```

This way there is **only one** for the whole application, and with it a single
Repository and a single dictionary. If we created a new one on every request, we would
start from an empty dictionary each time and the people inserted would vanish.
