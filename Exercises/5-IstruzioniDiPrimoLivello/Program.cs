// =====================================================================
// ESERCITAZIONE 5: istruzioni di primo livello (programmi senza Main)
// Corso C# e .NET 10 - RacingSystem
// EN: EXERCISE 5: top-level statements (programs without Main)
// EN: C# and .NET 10 course - RacingSystem
// =====================================================================
//
// Questo e' il file con le istruzioni di primo livello del progetto:
// non c'e' nessun metodo Main scritto a mano, lo genera il compilatore
// dentro una classe Program creata da lui. Un progetto puo' avere un
// solo file fatto cosi'.
// EN: This is the project's file with top-level statements: there is no
// EN: hand-written Main method, the compiler generates it inside a
// EN: Program class it creates itself. A project can have only one file
// EN: like this.


// ---------------------------------------------------------------------
// REGOLA 1
// Le direttive using devono venire prima di tutto il resto.
// "using static" permette di scrivere WriteLine al posto di
// Console.WriteLine.
// EN: RULE 1
// EN: using directives must come before everything else.
// EN: "using static" lets us write WriteLine instead of Console.WriteLine.
// ---------------------------------------------------------------------
using static System.Console;


// ---------------------------------------------------------------------
// REGOLA 2
// Da qui in poi si scrivono direttamente le istruzioni: niente classe,
// niente Main, niente parentesi graffe intorno.
// EN: RULE 2
// EN: From here on we write the statements directly: no class, no Main,
// EN: no curly braces around them.
// ---------------------------------------------------------------------
WriteLine("=== ISTRUZIONI DI PRIMO LIVELLO ===");
WriteLine("Il programma parte da qui: non serve scrivere il metodo Main.");
WriteLine();


// ---------------------------------------------------------------------
// REGOLA 3
// Gli argomenti della riga di comando restano disponibili nella
// variabile args, esattamente come in un Main scritto a mano.
// EN: RULE 3
// EN: The command line arguments are still available in the args
// EN: variable, exactly as in a hand-written Main.
// ---------------------------------------------------------------------
WriteLine($"Argomenti ricevuti: {args.Length}");
foreach (string argomento in args)
{
    WriteLine($"  - {argomento}");
}
WriteLine();


// ---------------------------------------------------------------------
// REGOLA 4
// Le istruzioni di primo livello stanno nello spazio dei nomi globale,
// quindi possono usare i tipi dichiarati in fondo al file.
// EN: RULE 4
// EN: Top-level statements live in the global namespace, so they can use
// EN: the types declared at the bottom of the file.
// ---------------------------------------------------------------------
Pilota pilota = new Pilota("Charles Leclerc", 16);
WriteLine(pilota.Presentati());
WriteLine();


// ---------------------------------------------------------------------
// REGOLA 5
// Per restituire un valore al sistema operativo si usa return, come in
// un Main che restituisce un int. 0 significa "tutto a posto".
// EN: RULE 5
// EN: To return a value to the operating system we use return, as in a
// EN: Main returning an int. 0 means "everything went fine".
// ---------------------------------------------------------------------
WriteLine("Fine del programma: restituisco 0.");
return 0;


// ---------------------------------------------------------------------
// REGOLA 6
// Spazi dei nomi e definizioni di tipi sono ammessi, ma solo DOPO le
// istruzioni di primo livello. Se spostassi questa classe in cima, il
// progetto non compilerebbe piu'.
// EN: RULE 6
// EN: Namespaces and type definitions are allowed, but only AFTER the
// EN: top-level statements. If you moved this class to the top, the
// EN: project would no longer compile.
// ---------------------------------------------------------------------
class Pilota
{
    public string Nome { get; }
    public int Numero { get; }

    public Pilota(string nome, int numero)
    {
        Nome = nome;
        Numero = numero;
    }

    public string Presentati()
    {
        return $"Sono {Nome} e corro con il numero {Numero}.";
    }
}
