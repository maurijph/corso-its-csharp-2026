# Mini guida a LINQ

**LINQ** è un insieme di metodi per lavorare sulle collezioni (array, liste, i
`Values` di un dizionario...) **senza scrivere cicli `foreach`**: filtrare, ordinare,
contare, cercare.

Per usarlo serve `using System.Linq;`, che nei progetti .NET moderni è già incluso
in automatico.

## I dati degli esempi

```csharp
List<Pilota> piloti = new List<Pilota>
{
    new Pilota { Id = 1, Nome = "Charles Leclerc", Scuderia = "Ferrari", Numero = 16 },
    new Pilota { Id = 2, Nome = "Lando Norris",    Scuderia = "McLaren", Numero = 4  },
    new Pilota { Id = 3, Nome = "Lewis Hamilton",  Scuderia = "Ferrari", Numero = 44 },
};
```

## Prima di tutto: la freccia `=>`

Quasi tutti i metodi LINQ vogliono una **lambda**, cioè una piccola funzione scritta
al volo:

```csharp
p => p.Scuderia == "Ferrari"
```

Si legge: *"dato un pilota `p`, restituisci vero se la sua scuderia è Ferrari"*.
LINQ prende i piloti della lista uno per uno, e ogni volta `p` è il pilota di turno.
È come il `p` di `foreach (Pilota p in piloti)`.

`p` è solo un nome: puoi scrivere anche `pilota => pilota.Scuderia == "Ferrari"`.

## I metodi più usati

| Metodo | Cosa fa | Esempio | Risultato |
|---|---|---|---|
| `Where` | tiene solo gli elementi che rispettano la condizione | `piloti.Where(p => p.Scuderia == "Ferrari")` | Leclerc, Hamilton |
| `Select` | trasforma ogni elemento in qualcos'altro | `piloti.Select(p => p.Nome)` | i soli nomi |
| `OrderBy` | ordina dal più piccolo al più grande | `piloti.OrderBy(p => p.Numero)` | Norris, Leclerc, Hamilton |
| `OrderByDescending` | ordina dal più grande al più piccolo | `piloti.OrderByDescending(p => p.Numero)` | Hamilton, Leclerc, Norris |
| `FirstOrDefault` | il primo che rispetta la condizione, o `null` se non c'è | `piloti.FirstOrDefault(p => p.Numero == 4)` | Norris |
| `Any` | c'è almeno un elemento che rispetta la condizione? | `piloti.Any(p => p.Numero == 44)` | `true` |
| `Count` | quanti elementi (che rispettano la condizione) | `piloti.Count(p => p.Scuderia == "Ferrari")` | `2` |
| `Max` / `Min` | il valore più grande / più piccolo | `piloti.Max(p => p.Numero)` | `44` |
| `Sum` / `Average` | somma / media | `piloti.Average(p => p.Numero)` | `21.33...` |

## Attenzione: `ToList()` e `ToArray()`

`Where`, `Select` e `OrderBy` **non restituiscono una lista**: restituiscono una
"ricetta" che verrà eseguita solo quando serve. Per ottenere una lista vera e propria
aggiungi in fondo `ToList()` o `ToArray()`:

```csharp
List<Pilota> ferrari = piloti.Where(p => p.Scuderia == "Ferrari").ToList();
```

`Any`, `Count`, `Max`, `FirstOrDefault`... invece restituiscono subito un valore
(`bool`, `int`, un pilota): lì `ToList()` non serve.

## Metterli in fila

I metodi si possono concatenare: il risultato di uno diventa l'ingresso del successivo.
Si leggono dall'alto in basso, come una frase.

```csharp
// I nomi dei piloti Ferrari, in ordine di numero
// EN: The names of the Ferrari drivers, ordered by number
string[] nomi = piloti
    .Where(p => p.Scuderia == "Ferrari")
    .OrderBy(p => p.Numero)
    .Select(p => p.Nome!)
    .ToArray();
// risultato: ["Charles Leclerc", "Lewis Hamilton"] — EN: result: ["Charles Leclerc", "Lewis Hamilton"]
```

## Con e senza LINQ

Lo stesso filtro scritto con un ciclo e con LINQ:

```csharp
// Senza LINQ
// EN: Without LINQ
List<Pilota> ferrari = new List<Pilota>();
foreach (Pilota p in piloti)
{
    if (p.Scuderia == "Ferrari")
        ferrari.Add(p);
}

// Con LINQ
// EN: With LINQ
List<Pilota> ferrari2 = piloti.Where(p => p.Scuderia == "Ferrari").ToList();
```

Il risultato è identico: LINQ è solo un modo più corto di scriverlo.

## Con un dizionario

Un `Dictionary` è come una rubrica: ogni elemento ha una **chiave**, che serve a
trovarlo, e un **valore**, che è il dato vero e proprio.

```csharp
Dictionary<int, Pilota> dizionario = new Dictionary<int, Pilota>();
```

Le parentesi angolari `< >` dicono **che tipo di dati** contiene il dizionario:

- `int` è il tipo della **chiave**: un numero intero, cioè l'`Id` del pilota;
- `Pilota` è il tipo del **valore**: il pilota.

Quindi `Dictionary<int, Pilota>` si legge *"un dizionario che, dato un numero, ti trova
un pilota"*. È la stessa idea di `List<Pilota>`, che si legge *"una lista di piloti"*.

```csharp
dizionario.Add(1, leclerc);        // con la chiave 1 (il suo Id) salvo Leclerc — EN: under key 1 (its Id) I store Leclerc
Pilota p = dizionario[1];          // con la chiave 1 lo ritrovo — EN: with key 1 I find it again
```

Per usare LINQ sui piloti, cioè sui **valori**, parti da `.Values`:

```csharp
bool numeroUsato = dizionario.Values.Any(p => p.Numero == 44);
```

---

## English version

# LINQ mini guide

**LINQ** is a set of methods to work on collections (arrays, lists, the `Values` of a
dictionary...) **without writing `foreach` loops**: filtering, sorting, counting,
searching.

To use it you need `using System.Linq;`, which modern .NET projects already include
automatically.

### The data used in the examples

```csharp
List<Pilota> piloti = new List<Pilota>
{
    new Pilota { Id = 1, Nome = "Charles Leclerc", Scuderia = "Ferrari", Numero = 16 },
    new Pilota { Id = 2, Nome = "Lando Norris",    Scuderia = "McLaren", Numero = 4  },
    new Pilota { Id = 3, Nome = "Lewis Hamilton",  Scuderia = "Ferrari", Numero = 44 },
};
```

### First of all: the `=>` arrow

Almost every LINQ method wants a **lambda**, that is a small function written on the fly:

```csharp
p => p.Scuderia == "Ferrari"
```

It reads: *"given a driver `p`, return true if their team is Ferrari"*.
LINQ takes the drivers in the list one by one, and each time `p` is the current driver.
It is like the `p` in `foreach (Pilota p in piloti)`.

`p` is just a name: you can also write `pilota => pilota.Scuderia == "Ferrari"`.

### The most used methods

| Method | What it does | Example | Result |
|---|---|---|---|
| `Where` | keeps only the elements that meet the condition | `piloti.Where(p => p.Scuderia == "Ferrari")` | Leclerc, Hamilton |
| `Select` | turns each element into something else | `piloti.Select(p => p.Nome)` | the names only |
| `OrderBy` | sorts from smallest to largest | `piloti.OrderBy(p => p.Numero)` | Norris, Leclerc, Hamilton |
| `OrderByDescending` | sorts from largest to smallest | `piloti.OrderByDescending(p => p.Numero)` | Hamilton, Leclerc, Norris |
| `FirstOrDefault` | the first one that meets the condition, or `null` if there is none | `piloti.FirstOrDefault(p => p.Numero == 4)` | Norris |
| `Any` | is there at least one element that meets the condition? | `piloti.Any(p => p.Numero == 44)` | `true` |
| `Count` | how many elements (meeting the condition) | `piloti.Count(p => p.Scuderia == "Ferrari")` | `2` |
| `Max` / `Min` | the largest / smallest value | `piloti.Max(p => p.Numero)` | `44` |
| `Sum` / `Average` | sum / average | `piloti.Average(p => p.Numero)` | `21.33...` |

### Watch out: `ToList()` and `ToArray()`

`Where`, `Select` and `OrderBy` **do not return a list**: they return a "recipe" that
will run only when needed. To get a real list add `ToList()` or `ToArray()` at the end:

```csharp
List<Pilota> ferrari = piloti.Where(p => p.Scuderia == "Ferrari").ToList();
```

`Any`, `Count`, `Max`, `FirstOrDefault`... instead return a value right away
(`bool`, `int`, a driver): there `ToList()` is not needed.

### Chaining them

Methods can be chained: the result of one becomes the input of the next.
They read from top to bottom, like a sentence.

```csharp
// I nomi dei piloti Ferrari, in ordine di numero
// EN: The names of the Ferrari drivers, ordered by number
string[] nomi = piloti
    .Where(p => p.Scuderia == "Ferrari")
    .OrderBy(p => p.Numero)
    .Select(p => p.Nome!)
    .ToArray();
// risultato: ["Charles Leclerc", "Lewis Hamilton"] — EN: result: ["Charles Leclerc", "Lewis Hamilton"]
```

### With and without LINQ

The same filter written with a loop and with LINQ:

```csharp
// Senza LINQ
// EN: Without LINQ
List<Pilota> ferrari = new List<Pilota>();
foreach (Pilota p in piloti)
{
    if (p.Scuderia == "Ferrari")
        ferrari.Add(p);
}

// Con LINQ
// EN: With LINQ
List<Pilota> ferrari2 = piloti.Where(p => p.Scuderia == "Ferrari").ToList();
```

The result is identical: LINQ is just a shorter way to write it.

### With a dictionary

A `Dictionary` is like an address book: each element has a **key**, used to find it,
and a **value**, which is the actual data.

```csharp
Dictionary<int, Pilota> dizionario = new Dictionary<int, Pilota>();
```

The angle brackets `< >` say **what type of data** the dictionary holds:

- `int` is the type of the **key**: a whole number, that is the driver's `Id`;
- `Pilota` is the type of the **value**: the driver.

So `Dictionary<int, Pilota>` reads *"a dictionary that, given a number, finds you a
driver"*. It is the same idea as `List<Pilota>`, which reads *"a list of drivers"*.

```csharp
dizionario.Add(1, leclerc);        // con la chiave 1 (il suo Id) salvo Leclerc — EN: under key 1 (its Id) I store Leclerc
Pilota p = dizionario[1];          // con la chiave 1 lo ritrovo — EN: with key 1 I find it again
```

To use LINQ on the drivers, that is on the **values**, start from `.Values`:

```csharp
bool numeroUsato = dizionario.Values.Any(p => p.Numero == 44);
```
