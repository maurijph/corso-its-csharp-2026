// La "forma" dei dati che l'API riceve e restituisce, come Person nell'esercitazione 8.
// Un JSON come { "id": 44, "nome": "Lewis Hamilton", "scuderia": "Ferrari", "numero": 44 }
// diventa un oggetto Pilota, e viceversa.
// EN: The "shape" of the data the API receives and returns, like Person in exercise 8.
// EN: A JSON such as { "id": 44, "nome": "Lewis Hamilton", "scuderia": "Ferrari", "numero": 44 }
// EN: becomes a Pilota object, and vice versa.
public class Pilota
{
    // Chiave che identifica il pilota nel dizionario.
    // Qui la scriviamo Id, con la maiuscola come vuole la convenzione C#:
    // nel JSON diventa comunque "id".
    // EN: Key identifying the driver in the dictionary.
    // EN: Here we write it Id, capitalised as the C# convention wants:
    // EN: in JSON it still becomes "id".
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Scuderia { get; set; }

    // Numero di gara, quello dipinto sulla macchina
    // EN: Race number, the one painted on the car
    public int Numero { get; set; }
}
