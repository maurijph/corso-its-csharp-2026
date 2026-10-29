# Esercitazione 2.1 — Menu delle scuderie

Riprendiamo gli **argomenti della riga di comando** visti nell'esercitazione 2.

Il programma stampa un menu con tre scuderie e poi scrive quale hai scelto,
leggendo la scelta da `args[0]`, cioè dal primo argomento passato al programma.

## Prova a eseguirlo

Entra nella cartella dell'esercitazione e lancia:

```bash
dotnet run -- 2
```

> Il `--` serve a dire a `dotnet` che quello che viene dopo non è per lui,
> ma va passato al nostro programma.

Funziona: stampa il menu e poi `Hai scelto McLaren`.

Adesso lancialo **senza scrivere niente dopo `dotnet run`**:

```bash
dotnet run
```

Il programma non stampa `Scelta non valida` come forse ti aspettavi: si **interrompe
con un errore**, una `IndexOutOfRangeException`.

## Che cosa è successo

`args` è l'array degli argomenti che abbiamo scritto dopo il nome del programma.

- Con `dotnet run -- 2` l'array contiene un elemento: `args[0]` vale `"2"`.
- Con `dotnet run` l'array è **vuoto**: `args[0]` non esiste, perché non c'è
  nemmeno il primo elemento.

Chiedere a un array un elemento che non c'è non dà `null` e non dà stringa vuota:
fa fallire il programma. Il nome dell'errore lo dice chiaramente —
*index out of range*, indice fuori dall'intervallo.

Questo è un errore classico: il programma funziona benissimo finché lo provi
"nel modo giusto", e si rompe appena qualcuno lo usa in modo diverso.

## Il tuo compito

1. Copia la cartella di questa esercitazione dentro `MyExercises/`
   (ricorda la regola d'oro: si scrive codice solo lì).
2. Correggi il programma in modo che, lanciato **senza argomenti**, non vada
   in errore ma stampi un messaggio chiaro, per esempio:

   ```
   Nessuna scuderia selezionata. Esempio d'uso: dotnet run -- 1
   ```

3. Verifica di nuovo tutti i casi: nessun argomento, `1`, `2`, `3`,
   e un valore qualsiasi come `9` o `pippo`.

**Suggerimento:** prima di leggere `args[0]`, controlla quanti elementi ci sono
dentro `args`. La proprietà `args.Length` ti dice la sua lunghezza.

## Per chi vuole andare avanti

- Fai in modo che la scelta non sia sensibile alle maiuscole, accettando anche
  il nome della scuderia (`dotnet run -- ferrari`).
- Sostituisci la catena di `if` / `else if` con un costrutto `switch`.
- Se vengono passati più argomenti (`dotnet run -- 1 2 3`), stampa la scelta
  per ognuno usando un ciclo.

---

## English version

# Exercise 2.1 — Teams menu

We go back to the **command line arguments** we saw in exercise 2.

The program prints a menu with three teams and then writes which one you chose,
reading the choice from `args[0]`, that is from the first argument passed to the program.

### Try to run it

Enter the exercise folder and launch:

```bash
dotnet run -- 2
```

> The `--` is there to tell `dotnet` that what comes after is not for it,
> but has to be passed on to our program.

It works: it prints the menu and then `Hai scelto McLaren`.

Now launch it **without writing anything after `dotnet run`**:

```bash
dotnet run
```

The program does not print `Scelta non valida` as you may have expected: it **stops
with an error**, an `IndexOutOfRangeException`.

### What happened

`args` is the array of the arguments we wrote after the name of the program.

- With `dotnet run -- 2` the array contains one element: `args[0]` is `"2"`.
- With `dotnet run` the array is **empty**: `args[0]` does not exist, because not
  even the first element is there.

Asking an array for an element that is not there does not give `null` and does not
give an empty string: it makes the program fail. The name of the error says it
clearly — *index out of range*.

This is a classic mistake: the program works perfectly as long as you try it
"the right way", and it breaks as soon as somebody uses it differently.

### Your task

1. Copy the folder of this exercise into `MyExercises/`
   (remember the golden rule: you only write code there).
2. Fix the program so that, when launched **without arguments**, it does not fail
   but prints a clear message, for example:

   ```
   Nessuna scuderia selezionata. Esempio d'uso: dotnet run -- 1
   ```

3. Check all the cases again: no argument, `1`, `2`, `3`,
   and any other value such as `9` or `pippo`.

**Hint:** before reading `args[0]`, check how many elements there are inside
`args`. The `args.Length` property tells you its length.

### For those who want to go further

- Make the choice case insensitive, accepting the name of the team as well
  (`dotnet run -- ferrari`).
- Replace the `if` / `else if` chain with a `switch` construct.
- If more arguments are passed (`dotnet run -- 1 2 3`), print the choice
  for each one of them using a loop.
