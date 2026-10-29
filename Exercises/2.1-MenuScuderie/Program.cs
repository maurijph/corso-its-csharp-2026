// =====================================================================
// ESERCITAZIONE 2.1: menu delle scuderie
// Corso C# e .NET 10 - RacingSystem
//
// Questo programma NON e' corretto: leggi il README.md e scopri perche'.
// EN: EXERCISE 2.1: teams menu
// EN: C# and .NET 10 course - RacingSystem
// EN:
// EN: This program is NOT correct: read README.md and find out why.
// =====================================================================

namespace MenuScuderie;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("SCUDERIE F1");
        Console.WriteLine("===========");
        Console.WriteLine("1. Ferrari");
        Console.WriteLine("2. McLaren");
        Console.WriteLine("3. Red Bull");

        // Nessun controllo su args.Length
        // EN: No check on args.Length
        if (args[0] == "1")
            Console.WriteLine("Hai scelto Ferrari");
        else if (args[0] == "2")
            Console.WriteLine("Hai scelto McLaren");
        else if (args[0] == "3")
            Console.WriteLine("Hai scelto Red Bull");
        else
            Console.WriteLine("Scelta non valida");
    }
}
