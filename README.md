# Corso ITS — Programmazione C# / .NET (2026)

Materiale didattico del corso: slide delle lezioni ed esercitazioni svolte in aula.

Docente: **Maurizio Luciani**

---

## Come scaricare il materiale

### Prima di tutto: hai Git?

Git è il programma che scarica il materiale e lo tiene aggiornato. Non devi impararlo:
in questo corso ti serviranno solo due comandi, `git clone` la prima volta e `git pull`
ogni volta che pubblico una lezione nuova.

**Controlla se ce l'hai già.** Apri un terminale (premi `Win + R`, scrivi `powershell`, premi Invio) e digita:

```powershell
git --version
```

- Se risponde con una riga tipo `git version 2.51.0.windows.1`, Git c'è: passa all'Opzione A.
- Se risponde *"git non è riconosciuto come nome di cmdlet..."* (o simile), Git non è installato: installalo come segue.

**Come installarlo (Windows).** Scarica l'installer da [git-scm.com/downloads](https://git-scm.com/downloads),
avvialo e clicca **Next** fino alla fine: le impostazioni predefinite vanno benissimo, non c'è
niente da configurare. In alternativa, da terminale:

```powershell
winget install --id Git.Git
```

Finita l'installazione **chiudi e riapri il terminale** (altrimenti non vede ancora il nuovo
programma) e ricontrolla con `git --version`.

### Opzione A — Usare Git (consigliata, si aggiorna da sola)

Scarica il materiale in `C:\Dev\`: usiamo tutti lo stesso percorso, così in aula i comandi e i percorsi sono uguali per tutti.

Se la cartella `C:\Dev\` non esiste, creala (da Esplora File oppure con `mkdir C:\Dev`). Poi apri un terminale e scrivi:

```powershell
cd C:\Dev
git clone https://github.com/maurijph/corso-its-csharp-2026.git
```

Al termine troverai il materiale in `C:\Dev\corso-its-csharp-2026`.

Da quel momento, per scaricare le lezioni nuove ti basta entrare nella cartella e scrivere:

```bash
git pull
```

### Opzione B — Scaricare tutto in un file ZIP (la più semplice ma è da poveracci)

1. In alto in questa pagina clicca il pulsante verde **`<> Code`**
2. Scegli **Download ZIP**
3. Estrai lo ZIP dove preferisci

> Lo ZIP è una *fotografia* del materiale di oggi: quando aggiungo nuove lezioni devi riscaricarlo.



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

---

## English version

# ITS Course — C# / .NET Programming (2026)

Teaching material for the course: lesson slides and exercises done in class.

Teacher: **Maurizio Luciani**

---

### How to download the material

#### First of all: do you have Git?

Git is the program that downloads the material and keeps it up to date. You do not have to
learn it: in this course you will only need two commands, `git clone` the first time and
`git pull` every time I publish a new lesson.

**Check whether you already have it.** Open a terminal (press `Win + R`, type `powershell`, press Enter) and type:

```powershell
git --version
```

- If it answers with a line such as `git version 2.51.0.windows.1`, Git is there: go to Option A.
- If it answers *"git is not recognized as the name of a cmdlet..."* (or similar), Git is not installed: install it as follows.

**How to install it (Windows).** Download the installer from [git-scm.com/downloads](https://git-scm.com/downloads),
run it and click **Next** all the way through: the default settings are perfectly fine, there is
nothing to configure. Alternatively, from a terminal:

```powershell
winget install --id Git.Git
```

When the installation is over **close and reopen the terminal** (otherwise it does not see the
new program yet) and check again with `git --version`.

#### Option A — Use Git (recommended, it updates itself)

Download the material into `C:\Dev\`: we all use the same path, so that in class the commands and the paths are the same for everyone.

If the `C:\Dev\` folder does not exist, create it (from File Explorer or with `mkdir C:\Dev`). Then open a terminal and type:

```powershell
cd C:\Dev
git clone https://github.com/maurijph/corso-its-csharp-2026.git
```

When it finishes you will find the material in `C:\Dev\corso-its-csharp-2026`.

From then on, to download the new lessons you just need to enter the folder and type:

```bash
git pull
```

#### Option B — Download everything as a ZIP file (the simplest one, but it is the poor man's way)

1. At the top of this page click the green **`<> Code`** button
2. Choose **Download ZIP**
3. Extract the ZIP wherever you prefer

> The ZIP is a *snapshot* of today's material: when I add new lessons you have to download it again.

---

### How the repository is organised

| Folder | What it contains |
|---|---|
| [`Slides/`](Slides/) | The slides of each lesson, as PDF |
| [`Exercises/`](Exercises/) | The code of the exercises done in class. One folder per exercise session, numbered in lesson order: `1-PrimoProgramma`, `2-ArgomentiRigaDiComando`, ... |
| [`Solutions/`](Solutions/) | The solutions of the exercises, published **after** the lesson |
| [`Resources/`](Resources/) | Useful links, cheat sheets, further reading |
| [`MyExercises/`](MyExercises/) | **Your working folder.** It stays on your computer: updates never touch it |

> **Golden rule:** only write code inside `MyExercises/`. I update the other folders
> at the end of the day, and your changes would be lost.
> You can find the instructions in [`MyExercises/README.md`](MyExercises/README.md).

---

### How to open the exercises

The exercises are **.NET projects in C#**. You need:

- The [.NET SDK](https://dotnet.microsoft.com/download) (version 10 or later)
- An editor: [Visual Studio Community](https://visualstudio.microsoft.com/it/vs/community/) (free) or [Visual Studio Code](https://code.visualstudio.com/) with the **C# Dev Kit** extension

To run an exercise from the terminal, enter its folder and launch:

```bash
dotnet run
```

---

### Questions and reports

Did you find a mistake in the slides? Does an exercise fail to compile? Is something unclear?

Open an **[Issue](../../issues)** on this repository: click on *Issues* → *New issue*, describe the problem and state which lesson it is about. I will answer there, so that the answer is useful to your classmates too.

---

### Terms of use

The material in this repository is meant **exclusively for the students of the course**, for personal study use.

Do not redistribute it and do not publish it elsewhere without asking the teacher first.
