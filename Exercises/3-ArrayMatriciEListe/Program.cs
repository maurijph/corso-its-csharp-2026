// =====================================================================
// METTIAMO IN PRATICA: array, matrici, liste e cicli
// Corso C# e .NET 10 - RacingSystem
// =====================================================================


// ---------------------------------------------------------------------
// ESERCIZIO 1
// Array di stringhe: contare quante volte compare un valore
// ---------------------------------------------------------------------
Console.WriteLine("=== ESERCIZIO 1: conteggio in un array ===");

// Vincitori delle ultime gare
string[] vincitori = { "Leclerc", "Norris", "Verstappen", "Leclerc", "Piastri", "Leclerc" };
string pilotaCercato = "Leclerc";
int conteggio = 0;

// Versione con il ciclo for: usiamo l'indice i per leggere ogni elemento
for (int i = 0; i < vincitori.Length; i++)
{
    if (vincitori[i] == pilotaCercato)
    {
        conteggio++;
    }
}

Console.WriteLine($"Gare analizzate: {vincitori.Length}");
Console.WriteLine($"{pilotaCercato} ha vinto {conteggio} volte (ciclo for).");

// Stessa logica con foreach: non serve l'indice
int conteggioForeach = 0;
foreach (string vincitore in vincitori)
{
    if (vincitore == pilotaCercato)
    {
        conteggioForeach++;
    }
}
Console.WriteLine($"{pilotaCercato} ha vinto {conteggioForeach} volte (ciclo foreach).");
Console.WriteLine();


// ---------------------------------------------------------------------
// ESERCIZIO 2
// Matrice quadrata: somma degli elementi sulla diagonale principale
// ---------------------------------------------------------------------
Console.WriteLine("=== ESERCIZIO 2: diagonale di una matrice ===");

// Punti ottenuti da 3 piloti (righe) in 3 gare (colonne)
int[,] punti =
{
    { 25, 18, 15 },
    { 12, 25, 10 },
    {  8,  6, 25 }
};

// Stampa della matrice, con due cicli annidati
for (int riga = 0; riga < punti.GetLength(0); riga++)
{
    for (int colonna = 0; colonna < punti.GetLength(1); colonna++)
    {
        Console.Write($"{punti[riga, colonna],4}");
    }
    Console.WriteLine();
}

// Sulla diagonale principale riga e colonna hanno lo stesso indice:
// punti[0,0], punti[1,1], punti[2,2] -> basta un solo ciclo
int sommaDiagonale = 0;
for (int i = 0; i < punti.GetLength(0); i++)
{
    sommaDiagonale += punti[i, i];
}

Console.WriteLine($"Somma della diagonale principale: {sommaDiagonale}");
Console.WriteLine();


// ---------------------------------------------------------------------
// ESERCIZIO 3
// Lista di stringhe: cercare una parola con foreach
// ---------------------------------------------------------------------
Console.WriteLine("=== ESERCIZIO 3: ricerca in una lista ===");

List<string> scuderie = new List<string> { "Ferrari", "McLaren", "Mercedes", "Red Bull", "Williams" };

// A differenza dell'array, la lista può crescere
scuderie.Add("Aston Martin");

string scuderiaCercata = "McLaren";
bool trovata = false;

foreach (string scuderia in scuderie)
{
    if (scuderia == scuderiaCercata)
    {
        trovata = true;
        break; // trovata: inutile continuare a cercare
    }
}

if (trovata)
{
    Console.WriteLine($"La scuderia {scuderiaCercata} è presente nella lista.");
}
else
{
    Console.WriteLine($"La scuderia {scuderiaCercata} NON è presente nella lista.");
}

// Prova con un valore assente
string scuderiaAssente = "Minardi";
bool trovataAssente = false;
foreach (string scuderia in scuderie)
{
    if (scuderia == scuderiaAssente)
    {
        trovataAssente = true;
        break;
    }
}
Console.WriteLine(trovataAssente
    ? $"La scuderia {scuderiaAssente} è presente nella lista."
    : $"La scuderia {scuderiaAssente} NON è presente nella lista.");
Console.WriteLine();


// ---------------------------------------------------------------------
// ESERCIZIO 4
// Lista di interi: media degli elementi con foreach
// ---------------------------------------------------------------------
Console.WriteLine("=== ESERCIZIO 4: media di una lista ===");

// Potenza in cavalli di alcune auto
List<int> cavalli = new List<int> { 950, 1000, 980, 1010, 990 };

int somma = 0;
foreach (int cv in cavalli)
{
    somma += cv;
}

// (double) è necessario: una divisione tra due int darebbe un int
// e perderemmo i decimali
double media = (double)somma / cavalli.Count;

Console.WriteLine($"Potenze: {string.Join(", ", cavalli)}");
Console.WriteLine($"Somma: {somma} CV");
Console.WriteLine($"Media: {media:F2} CV");
