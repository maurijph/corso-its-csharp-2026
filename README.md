# Corso ITS — Programmazione C# / .NET (2026)

Materiale didattico del corso: slide delle lezioni ed esercitazioni svolte in aula.

Docente: **Maurizio Luciani**

---

## Come scaricare il materiale

### Opzione A — Scaricare tutto in un file ZIP (la più semplice)

1. In alto in questa pagina clicca il pulsante verde **`<> Code`**
2. Scegli **Download ZIP**
3. Estrai lo ZIP dove preferisci

> Lo ZIP è una *fotografia* del materiale di oggi: quando aggiungo nuove lezioni devi riscaricarlo.

### Opzione B — Usare Git (consigliata, si aggiorna da sola)

Se hai [Git](https://git-scm.com/downloads) installato, apri un terminale nella cartella dove vuoi il materiale e scrivi:

```bash
git clone https://github.com/maurijph/corso-its-csharp-2026.git
```

Da quel momento, per scaricare le lezioni nuove ti basta entrare nella cartella e scrivere:

```bash
git pull
```

---

## Com'è organizzato il repository

| Cartella | Cosa contiene |
|---|---|
| [`Slides/`](Slides/) | Le slide di ogni lezione, in PDF |
| [`Exercises/`](Exercises/) | Il codice degli esercizi svolti in aula. Una cartella per esercitazione, numerata in ordine di lezione: `1-PrimoProgramma`, `2-ArgomentiRigaDiComando`, ... |
| [`Solutions/`](Solutions/) | Le soluzioni degli esercizi, pubblicate **dopo** la lezione |
| [`Resources/`](Resources/) | Link utili, cheat sheet, materiale di approfondimento |
| [`MyExercises/`](MyExercises/) | **La tua cartella di lavoro.** Resta sul tuo computer: gli aggiornamenti non la toccano mai |

> **Regola d'oro:** scrivi codice solo dentro `MyExercises/`. Le altre cartelle
> le aggiorno io a fine giornata, e le tue modifiche andrebbero perse.
> Trovi le istruzioni in [`MyExercises/README.md`](MyExercises/README.md).

---

## Come aprire le esercitazioni

Le esercitazioni sono progetti **.NET in C#**. Ti serve:

- [.NET SDK](https://dotnet.microsoft.com/download) (versione 10 o superiore)
- Un editor: [Visual Studio Community](https://visualstudio.microsoft.com/it/vs/community/) (gratuito) oppure [Visual Studio Code](https://code.visualstudio.com/) con l'estensione **C# Dev Kit**

Per eseguire un'esercitazione da terminale, entra nella sua cartella e lancia:

```bash
dotnet run
```

---

## Domande e segnalazioni

Hai trovato un errore nelle slide? Un esercizio non compila? Qualcosa non è chiaro?

Apri una **[Issue](../../issues)** su questo repository: clicca su *Issues* → *New issue*, descrivi il problema e indica di quale lezione si tratta. Risponderò lì, così la risposta è utile anche ai tuoi compagni.

---

## Licenza d'uso

Il materiale di questo repository è destinato **esclusivamente agli studenti del corso** per uso personale di studio.

Non ridistribuirlo e non pubblicarlo altrove senza chiedere prima al docente.
