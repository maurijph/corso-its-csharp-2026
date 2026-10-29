# Le tue esercitazioni

Questa cartella è **tua**. Tutto quello che ci metti dentro resta solo sul tuo
computer: non finisce su GitHub e io non lo vedo.

Soprattutto: quando a fine giornata carico le esercitazioni nuove e tu scarichi
gli aggiornamenti, **il tuo lavoro qui dentro non viene toccato**.

---

## La regola importante

> **Non modificare i file dentro `Exercises/`.**
> Quella cartella è mia: la aggiorno io, e le tue modifiche andrebbero perse
> o ti impedirebbero di scaricare le lezioni nuove.

Se vuoi partire da un esercizio che abbiamo fatto insieme, **copialo** qui dentro
e lavora sulla copia.

---

## Creare un progetto nuovo

Apri un terminale in questa cartella e scrivi (cambiando il nome):

```bash
dotnet new console -o MioEsercizio
cd MioEsercizio
dotnet run
```

Puoi organizzarti come preferisci: una sottocartella per giornata, per argomento,
come ti è più comodo.

---

## "git pull" mi dà errore, cosa faccio?

Se vedi un messaggio tipo *"Your local changes would be overwritten"*, vuol dire
che hai modificato per sbaglio un file **mio** (dentro `Exercises/`, `Slides/`
o `Resources/`).

Per rimettere tutto a posto e buttare via quelle modifiche:

```bash
git checkout -- Exercises Slides Resources
git pull
```

Questo comando **non tocca** la cartella `MyExercises/`: il tuo lavoro è al
sicuro.

Se non sei sicuro di cosa fare, chiedimelo prima di eseguire comandi a caso.

---

## English version

# Your exercises

This folder is **yours**. Everything you put in it stays only on your
computer: it does not end up on GitHub and I do not see it.

Above all: when at the end of the day I upload the new exercises and you download
the updates, **your work in here is not touched**.

---

### The important rule

> **Do not modify the files inside `Exercises/`.**
> That folder is mine: I update it, and your changes would be lost
> or would stop you from downloading the new lessons.

If you want to start from an exercise we did together, **copy it** in here
and work on the copy.

---

### Creating a new project

Open a terminal in this folder and type (changing the name):

```bash
dotnet new console -o MioEsercizio
cd MioEsercizio
dotnet run
```

You can organise yourself as you prefer: one subfolder per day, per topic,
whatever is more convenient for you.

---

### "git pull" gives me an error, what do I do?

If you see a message such as *"Your local changes would be overwritten"*, it means
that you modified by mistake a file of **mine** (inside `Exercises/`, `Slides/`
or `Resources/`).

To put everything back in place and throw those changes away:

```bash
git checkout -- Exercises Slides Resources
git pull
```

This command **does not touch** the `MyExercises/` folder: your work is
safe.

If you are not sure what to do, ask me before running random commands.
