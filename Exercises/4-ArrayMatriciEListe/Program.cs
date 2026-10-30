// =====================================================================
// METTIAMO IN PRATICA: array, matrici, liste e cicli
// Corso C# e .NET 10 - RacingSystem
// EN: LET'S PRACTISE: arrays, matrices, lists and loops
// EN: C# and .NET 10 course - RacingSystem
// =====================================================================

// ---------------------------------------------------------------------
// ESERCIZIO 1
// Array di stringhe: contare quante volte compare un valore
// EN: EXERCISE 1
// EN: Array of strings: count how many times a value appears
// ---------------------------------------------------------------------
Console.WriteLine("=== ESERCIZIO 1: conteggio in un array ===");

// Vincitori delle ultime gare
// EN: Winners of the most recent races
string[] vincitori = { "Leclerc", "Norris", "Verstappen", "Leclerc", "Piastri", "Leclerc" };
string pilotaCercato = "Leclerc";
int conteggio = 0;

// Versione con il ciclo for: usiamo l'indice i per leggere ogni elemento
// EN: Version with the for loop: we use the index i to read every element
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
// EN: Same logic with foreach: the index is not needed
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
// EN: EXERCISE 2
// EN: Square matrix: sum of the elements on the main diagonal
// ---------------------------------------------------------------------
Console.WriteLine("=== ESERCIZIO 2: diagonale di una matrice ===");

// Punti ottenuti da 3 piloti (righe) in 3 gare (colonne)
// EN: Points scored by 3 drivers (rows) in 3 races (columns)
int[,] punti =
{
    { 25, 18, 15 },
    { 12, 25, 10 },
    {  8,  6, 25 }
};

// Stampa della matrice, con due cicli annidati
// EN: Printing the matrix, with two nested loops
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
// EN: On the main diagonal row and column have the same index:
// EN: punti[0,0], punti[1,1], punti[2,2] -> a single loop is enough
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
// EN: EXERCISE 3
// EN: List of strings: searching for a word with foreach
// ---------------------------------------------------------------------
Console.WriteLine("=== ESERCIZIO 3: ricerca in una lista ===");

List<string> scuderie = new List<string> { "Ferrari", "McLaren", "Mercedes", "Red Bull", "Williams" };

// A differenza dell'array, la lista può crescere
// EN: Unlike the array, the list can grow
scuderie.Add("Aston Martin");

string scuderiaCercata = "McLaren";
bool trovata = false;

foreach (string scuderia in scuderie)
{
    if (scuderia == scuderiaCercata)
    {
        trovata = true;
        break; // trovata: inutile continuare a cercare — EN: found: no point in keeping on searching
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
// EN: Try with a value that is not there
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
// EN: EXERCISE 4
// EN: List of integers: average of the elements with foreach
// ---------------------------------------------------------------------
Console.WriteLine("=== ESERCIZIO 4: media di una lista ===");

// Potenza in cavalli di alcune auto
// EN: Power in horsepower of some cars
List<int> cavalli = new List<int> { 950, 1000, 980, 1010, 990 };

int somma = 0;
foreach (int cv in cavalli)
{
    somma += cv;
}

// (double) è necessario: una divisione tra due int darebbe un int
// e perderemmo i decimali
// EN: (double) is necessary: a division between two ints would give an int
// EN: and we would lose the decimals
double media = (double)somma / cavalli.Count;

Console.WriteLine($"Potenze: {string.Join(", ", cavalli)}");
Console.WriteLine($"Somma: {somma} CV");
Console.WriteLine($"Media: {media:F2} CV");
