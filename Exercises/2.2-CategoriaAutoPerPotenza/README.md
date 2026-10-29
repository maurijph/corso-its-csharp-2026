# Esercitazione 2.2 — Categoria dell'auto in base alla potenza

> **Questo esercizio devi svolgerlo tu.** Nella cartella trovi solo la traccia:
> [`Program.cs`](Program.cs) contiene i commenti con i passi da seguire, il codice
> va scritto da te.

## Che cosa deve fare il programma

Il programma riceve **due argomenti** dalla riga di comando: il nome di un'auto
e la sua potenza in cavalli.

```bash
dotnet run -- SF-25 1000
```

In base alla potenza stampa la categoria dell'auto:

| Potenza | Categoria |
|---|---|
| meno di 200 CV | `Auto stradale` |
| da 200 a 599 CV | `Auto sportiva` |
| 600 CV o più | `Auto da corsa` |

Il risultato è il nome dell'auto seguito dalla categoria:

```
SF-25: Auto da corsa
```

## Suggerimento

`args` contiene **solo stringhe**: `"1000"` è un testo, non un numero, e con un testo
non puoi fare confronti come *maggiore* o *minore*. Prima di usarla, la potenza va
convertita in numero con `int.Parse`:

```csharp
string nome = args[0];
int potenza = int.Parse(args[1]);
```

Da qui in poi `potenza` è un numero vero e puoi confrontarlo con `200` e `600`
usando `if` / `else if` / `else`, come abbiamo visto a lezione.

## Come lo provi

Lancialo almeno una volta per ogni categoria, per essere sicuro che tutti e tre
i rami funzionino:

```bash
dotnet run -- Panda 69
dotnet run -- Giulia 280
dotnet run -- SF-25 1000
```

Confronta quello che ottieni con [`OUTPUT_ATTESO.txt`](OUTPUT_ATTESO.txt).

## Domanda a cui rispondere

Adesso prova a scrivere la potenza **a parole** invece che in cifre:

```bash
dotnet run -- SF-25 mille
```

Che cosa succede? Il programma stampa una categoria, non stampa niente, o fa
qualcos'altro? Leggi con attenzione quello che compare a schermo e **annota il nome
dell'errore**: ne parliamo insieme in aula.

> È l'inizio di un discorso che riprenderemo più avanti con le API: i dati che
> arrivano da fuori — riga di comando, tastiera, rete — non sono mai affidabili,
> e vanno sempre controllati prima di usarli.

## Per chi vuole andare avanti

- Come nell'esercitazione 2.1, controlla `args.Length` prima di leggere gli
  argomenti: che cosa deve fare il programma se gli argomenti sono zero o uno solo?
- Stampa anche la potenza nel messaggio finale, per esempio
  `SF-25 (1000 CV): Auto da corsa`.
- Che cosa succede con una potenza negativa, `dotnet run -- Fantasma -50`?
  Ha senso il risultato? Come lo gestiresti?

---

## English version

# Exercise 2.2 — Car category based on its power

> **You have to do this exercise yourself.** In the folder you only find the outline:
> [`Program.cs`](Program.cs) contains the comments with the steps to follow, the code
> has to be written by you.

### What the program has to do

The program receives **two arguments** from the command line: the name of a car
and its power in horsepower.

```bash
dotnet run -- SF-25 1000
```

Based on the power it prints the category of the car:

| Power | Category |
|---|---|
| less than 200 HP | `Auto stradale` |
| from 200 to 599 HP | `Auto sportiva` |
| 600 HP or more | `Auto da corsa` |

The result is the name of the car followed by the category:

```
SF-25: Auto da corsa
```

### Hint

`args` contains **only strings**: `"1000"` is a text, not a number, and with a text
you cannot make comparisons such as *greater than* or *less than*. Before using it,
the power has to be converted into a number with `int.Parse`:

```csharp
string nome = args[0];
int potenza = int.Parse(args[1]);
```

From here on `potenza` is a real number and you can compare it with `200` and `600`
using `if` / `else if` / `else`, as we saw during the lesson.

### How to test it

Launch it at least once for each category, to be sure that all three branches work:

```bash
dotnet run -- Panda 69
dotnet run -- Giulia 280
dotnet run -- SF-25 1000
```

Compare what you get with [`OUTPUT_ATTESO.txt`](OUTPUT_ATTESO.txt).

### A question to answer

Now try to write the power **in words** instead of in figures:

```bash
dotnet run -- SF-25 mille
```

What happens? Does the program print a category, print nothing, or do something
else? Read carefully what appears on the screen and **write down the name of the
error**: we will talk about it together in class.

> It is the beginning of a subject we will go back to later on with the APIs: the
> data that comes from outside — command line, keyboard, network — is never
> trustworthy, and must always be checked before being used.

### For those who want to go further

- As in exercise 2.1, check `args.Length` before reading the arguments: what should
  the program do if there are zero arguments, or only one?
- Print the power in the final message too, for example
  `SF-25 (1000 CV): Auto da corsa`.
- What happens with a negative power, `dotnet run -- Fantasma -50`?
  Does the result make sense? How would you handle it?
