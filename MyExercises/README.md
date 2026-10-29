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
