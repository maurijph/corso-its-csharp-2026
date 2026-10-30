// =====================================================================
// ESERCITAZIONE 6: programmazione a oggetti
// Corso C# e .NET 10 - RacingSystem
// EN: EXERCISE 6: object oriented programming
// EN: C# and .NET 10 course - RacingSystem
//
// Un linguaggio e' "ad oggetti" quando con la sua sintassi permette di
// implementare incapsulamento, ereditarieta' e polimorfismo: qui sotto
// c'e' un esempio minimo di ognuno dei tre.
// EN: A language is "object oriented" when its syntax makes it possible
// EN: to implement encapsulation, inheritance and polymorphism: below
// EN: there is a minimal example of each of the three.
// =====================================================================


// ---------------------------------------------------------------------
// ESERCIZIO 1
// Incapsulamento: lo stato interno si cambia solo dai metodi pubblici
// EN: EXERCISE 1
// EN: Encapsulation: the internal state is changed only by public methods
// ---------------------------------------------------------------------
using System;
using System.Collections.Generic;

Console.WriteLine("=== ESERCIZIO 1: incapsulamento ===");

Motore motore = new Motore();

// Da fuori il campo _giri non e' raggiungibile: si passa dal metodo,
// che e' l'unica "porta" aperta sull'oggetto (la black-box)
// EN: From outside, the _giri field cannot be reached: we go through the
// EN: method, which is the only open "door" on the object (the black-box)
motore.Accelera(5000);
Console.WriteLine($"Dopo Accelera(5000): {motore.Giri} giri/min");

// L'oggetto si difende da solo: oltre il limite non va, qualunque cosa
// gli chieda chi lo usa
// EN: The object defends itself: it does not go past the limit, whatever
// EN: the one using it asks for
motore.Accelera(100000);
Console.WriteLine($"Dopo Accelera(100000): {motore.Giri} giri/min (limitato)");
Console.WriteLine();


// ---------------------------------------------------------------------
// ESERCIZIO 2
// Ereditarieta': una classe derivata riusa cio' che c'e' nella base
// EN: EXERCISE 2
// EN: Inheritance: a derived class reuses what is in the base class
// ---------------------------------------------------------------------
Console.WriteLine("=== ESERCIZIO 2: ereditarietà ===");

Veicolo veicolo = new Veicolo("Panda di servizio");
AutoDaCorsa auto = new AutoDaCorsa("Ferrari SF-26", 1000);
Formula1 monoposto = new Formula1("Red Bull RB22", 1050, 1);

// Presentati() e' scritto una volta sola, nella classe base Veicolo:
// tutte le derivate ce l'hanno senza riscriverlo
// EN: Presentati() is written only once, in the Veicolo base class: all
// EN: the derived classes have it without rewriting it
Console.WriteLine(veicolo.Presentati());
Console.WriteLine(auto.Presentati());

// Formula1 eredita da AutoDaCorsa che eredita da Veicolo: l'ereditarieta'
// e' transitiva, quindi Formula1 ha anche i membri di Veicolo
// EN: Formula1 inherits from AutoDaCorsa which inherits from Veicolo:
// EN: inheritance is transitive, so Formula1 also has Veicolo's members
Console.WriteLine(monoposto.Presentati());
Console.WriteLine($"Cavalli (ereditati da AutoDaCorsa): {monoposto.Cavalli}");
Console.WriteLine();


// ---------------------------------------------------------------------
// ESERCIZIO 3
// Polimorfismo: stessa chiamata, comportamento diverso a seconda
// del tipo reale dell'oggetto
// EN: EXERCISE 3
// EN: Polymorphism: same call, different behaviour depending on the
// EN: real type of the object
// ---------------------------------------------------------------------
Console.WriteLine("=== ESERCIZIO 3: polimorfismo ===");

// Primo aspetto: gli oggetti delle classi derivate sono trattati come
// oggetti della classe base, quindi stanno tutti in una List<Veicolo>
// EN: First aspect: objects of the derived classes are treated as objects
// EN: of the base class, so they all fit in a single List<Veicolo>
List<Veicolo> garage = new List<Veicolo> { veicolo, auto, monoposto };

// Secondo aspetto: Descrivi() e' virtual nella base e override nelle
// derivate. A ogni giro il CLR guarda il tipo vero dell'oggetto e chiama
// la versione giusta, anche se la variabile v e' dichiarata Veicolo
// EN: Second aspect: Descrivi() is virtual in the base class and override
// EN: in the derived ones. On every iteration the CLR looks at the real
// EN: type of the object and calls the right version, even though the v
// EN: variable is declared as Veicolo
foreach (Veicolo v in garage)
{
    Console.WriteLine(v.Descrivi());
}

return 0;


// =====================================================================
// LE CLASSI
// Stanno in fondo al file perche' con le istruzioni di primo livello
// le definizioni dei tipi vanno dopo il codice (vedi esercitazione 5)
// EN: THE CLASSES
// EN: They are at the bottom of the file because with top-level
// EN: statements type definitions come after the code (see exercise 5)
// =====================================================================

// --- INCAPSULAMENTO ---
// EN: --- ENCAPSULATION ---
class Motore
{
    // private: visibile solo dentro la classe
    // EN: private: visible only inside the class
    private int _giri = 0;

    // Proprieta' di sola lettura: da fuori si puo' guardare, non toccare
    // EN: Read-only property: from outside you can look, not touch
    public int Giri
    {
        get { return _giri; }
    }

    // Metodo pubblico: e' qui che si controlla che lo stato resti valido
    // EN: Public method: this is where we check that the state stays valid
    public void Accelera(int giri)
    {
        if (giri > 15000)
        {
            giri = 15000; // limite del motore — EN: engine limit
        }

        _giri = giri;
    }
}

// --- EREDITARIETÀ E POLIMORFISMO ---
// EN: --- INHERITANCE AND POLYMORPHISM ---

// Classe base
// EN: Base class
class Veicolo
{
    public string Nome { get; }

    public Veicolo(string nome)
    {
        Nome = nome;
    }

    public string Presentati()
    {
        return $"Mi chiamo {Nome}.";
    }

    // virtual: le classi derivate possono darne una versione propria
    // EN: virtual: derived classes may give their own version of it
    public virtual string Descrivi()
    {
        return $"{Nome}: un veicolo come tanti.";
    }
}

// Classe derivata: ": Veicolo" vuol dire "eredita da Veicolo"
// EN: Derived class: ": Veicolo" means "inherits from Veicolo"
class AutoDaCorsa : Veicolo
{
    public int Cavalli { get; }

    // base(nome) passa il nome al costruttore della classe base
    // EN: base(nome) passes the name to the base class constructor
    public AutoDaCorsa(string nome, int cavalli) : base(nome)
    {
        Cavalli = cavalli;
    }

    // override: sostituisce la versione della classe base
    // EN: override: replaces the base class version
    public override string Descrivi()
    {
        return $"{Nome}: auto da corsa da {Cavalli} CV.";
    }
}

// Una classe derivata ha una sola classe base diretta (qui AutoDaCorsa),
// ma eredita comunque anche da Veicolo
// EN: A derived class has only one direct base class (here AutoDaCorsa),
// EN: but it still inherits from Veicolo as well
class Formula1 : AutoDaCorsa
{
    public int Numero { get; }

    public Formula1(string nome, int cavalli, int numero) : base(nome, cavalli)
    {
        Numero = numero;
    }

    public override string Descrivi()
    {
        return $"{Nome}: monoposto di Formula 1 numero {Numero}, {Cavalli} CV.";
    }
}
