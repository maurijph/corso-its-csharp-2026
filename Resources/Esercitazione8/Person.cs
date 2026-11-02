// La "forma" dei dati che l'API riceve e restituisce.
// Quando un client manda un JSON come { "id": 1, "name": "Mario", "age": 30 },
// ASP.NET Core lo trasforma da solo in un oggetto Person (deserializzazione);
// quando restituiamo una Person, la ritrasforma in JSON (serializzazione).
// Per farlo servono proprietà pubbliche con get e set.
// EN: The "shape" of the data the API receives and returns.
// EN: When a client sends a JSON such as { "id": 1, "name": "Mario", "age": 30 },
// EN: ASP.NET Core turns it into a Person object by itself (deserialization);
// EN: when we return a Person, it turns it back into JSON (serialization).
// EN: To do that it needs public properties with get and set.
public class Person
{
    // Chiave che identifica la persona nel dizionario.
    // Nel JSON i nomi diventano camelCase ("id", "name", "age") e in lettura
    // non conta maiuscolo/minuscolo: per questo "id" minuscolo funziona lo stesso
    // (per convenzione C# andrebbe scritto Id).
    // EN: Key identifying the person in the dictionary.
    // EN: In JSON the names become camelCase ("id", "name", "age") and when reading
    // EN: upper/lower case does not matter: that is why lowercase "id" still works
    // EN: (by C# convention it should be written Id).
    public int id { get; set; }

    // string? = il nome può mancare (null) senza che il compilatore protesti
    // EN: string? = the name may be missing (null) without the compiler complaining
    public string? Name { get; set; }

    public int Age { get; set; }
}
