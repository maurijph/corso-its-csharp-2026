# Esercitazione 3 — Usare un pacchetto NuGet (Humanizer)

> **Questo esercizio devi svolgerlo tu.** In [`Program.cs`](Program.cs) trovi la
> traccia con i passi da seguire: il codice va scritto da te.

## Che cos'è NuGet

Fino adesso hai usato solo quello che .NET porta con sé: `Console`, `string`,
`List`, e così via. Ma non tutto quello che serve è già nella scatola, e soprattutto
non ha senso riscrivere da zero cose che qualcun altro ha già scritto e collaudato.

**NuGet** è il magazzino da cui si prendono queste cose già pronte. Un *pacchetto*
NuGet è una libreria di codice scritta da altri, che scarichi e usi nel tuo progetto
con un comando solo. Esistono pacchetti per leggere file Excel, per parlare con un
database, per disegnare grafici: qualunque cosa ti serva, quasi sempre qualcuno
l'ha già fatta.

## Che cos'è Humanizer

**Humanizer** è un pacchetto per la manipolazione e la visualizzazione di stringhe,
enum, date, orari, intervalli di tempo, numeri e quantità.

In questa esercitazione ne usiamo un solo metodo, `Pluralize`, che restituisce il
plurale della stringa su cui è applicato.

La sua **firma** è:

```csharp
string string.Pluralize([bool inputIsKnownToBeSingular = true])
```

Letta a pezzi, dice tre cose:

- il primo `string` è il **tipo restituito**: il metodo ti ridà una stringa;
- `string.Pluralize` vuol dire che il metodo si chiama **su una stringa**, come se
  fosse sempre stato parte di `string`, anche se arriva dal pacchetto;
- fra parentesi c'è un parametro **facoltativo**: le quadre `[ ]` e il `= true`
  dicono che se non lo scrivi vale `true`. Per questo possiamo chiamarlo
  semplicemente `Pluralize()`, senza passare niente.

## Il tuo compito

**1.** Copia la cartella di questa esercitazione dentro `MyExercises/` e aprila
con il terminale (ricorda la regola d'oro: si scrive codice solo lì).

**2.** Aggiungi il pacchetto:

```bash
dotnet add package Humanizer
```

Apri adesso il file `.csproj` e guarda che cosa è cambiato: è comparso un blocco
simile a questo, che prima non c'era.

```xml
<ItemGroup>
  <PackageReference Include="Humanizer" Version="3.0.10" />
</ItemGroup>
```

È lì che .NET tiene l'elenco dei pacchetti che servono al progetto. Il numero di
versione che vedi tu può essere diverso: va benissimo lo stesso.

**3.** In [`Program.cs`](Program.cs) togli il commento alla riga `using Humanizer;`.
Serve a dire a C# dove andare a cercare il metodo `Pluralize`: senza, il
compilatore non lo trova, anche se il pacchetto è installato.

**4.** Scrivi la riga che stampa a console il plurale della parola `"Woman"`,
chiamando su di essa il metodo `Pluralize()`.

**5.** Esegui l'applicazione:

```bash
dotnet run
```

A console deve comparire la scritta:

```
Women
```

> Nota che non è `Womans`: Humanizer conosce i plurali irregolari dell'inglese.
> È esattamente il tipo di lavoro noioso per cui conviene usare un pacchetto già
> fatto, invece di scriverselo.

## Per chi vuole andare avanti

- Prova `Pluralize()` su altre parole: `"car"`, `"box"`, `"child"`, `"Ferrari"`.
  Indovini il risultato prima di eseguire?
- Esiste anche il metodo opposto, `Singularize()`. Prova `"Women".Singularize()`.
- Humanizer sa fare molto altro. Prova `5.ToWords()` e `3.Ordinalize()`:
  che cosa stampano?
- Vai su [nuget.org](https://www.nuget.org/) e cerca `Humanizer`: guarda quante
  volte è stato scaricato e da quanto tempo esiste.

---

## English version

# Exercise 3 — Using a NuGet package (Humanizer)

> **You have to do this exercise yourself.** In [`Program.cs`](Program.cs) you find
> the outline with the steps to follow: the code has to be written by you.

### What NuGet is

So far you have only used what .NET brings with it: `Console`, `string`, `List`,
and so on. But not everything you need is already in the box, and above all it
makes no sense to rewrite from scratch things that somebody else has already
written and tested.

**NuGet** is the warehouse these ready-made things are taken from. A NuGet
*package* is a code library written by others, that you download and use in your
project with a single command. There are packages to read Excel files, to talk to
a database, to draw charts: whatever you need, almost always somebody has already
done it.

### What Humanizer is

**Humanizer** is a package for the manipulation and the display of strings, enums,
dates, times, time spans, numbers and quantities.

In this exercise we use a single one of its methods, `Pluralize`, which returns the
plural of the string it is applied to.

Its **signature** is:

```csharp
string string.Pluralize([bool inputIsKnownToBeSingular = true])
```

Read piece by piece, it says three things:

- the first `string` is the **return type**: the method gives you back a string;
- `string.Pluralize` means that the method is called **on a string**, as if it had
  always been part of `string`, even though it comes from the package;
- inside the brackets there is an **optional** parameter: the square brackets
  `[ ]` and the `= true` say that if you do not write it, it is `true`. This is
  why we can simply call it as `Pluralize()`, passing nothing.

### Your task

**1.** Copy the folder of this exercise into `MyExercises/` and open it with the
terminal (remember the golden rule: you only write code there).

**2.** Add the package:

```bash
dotnet add package Humanizer
```

Now open the `.csproj` file and look at what changed: a block like this one, which
was not there before, has appeared.

```xml
<ItemGroup>
  <PackageReference Include="Humanizer" Version="3.0.10" />
</ItemGroup>
```

That is where .NET keeps the list of the packages the project needs. The version
number you see may be different: that is perfectly fine.

**3.** In [`Program.cs`](Program.cs) uncomment the `using Humanizer;` line. It tells
C# where to go and look for the `Pluralize` method: without it, the compiler does
not find it, even though the package is installed.

**4.** Write the line that prints to the console the plural of the word `"Woman"`,
by calling the `Pluralize()` method on it.

**5.** Run the application:

```bash
dotnet run
```

The console must show the text:

```
Women
```

> Note that it is not `Womans`: Humanizer knows the irregular plurals of English.
> It is exactly the kind of boring work for which it is worth using a ready-made
> package, instead of writing it yourself.

### For those who want to go further

- Try `Pluralize()` on other words: `"car"`, `"box"`, `"child"`, `"Ferrari"`.
  Can you guess the result before running it?
- There is also the opposite method, `Singularize()`. Try `"Women".Singularize()`.
- Humanizer can do much more. Try `5.ToWords()` and `3.Ordinalize()`:
  what do they print?
- Go to [nuget.org](https://www.nuget.org/) and search for `Humanizer`: look at how
  many times it has been downloaded and how long it has been around.
